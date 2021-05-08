import { aws_iam as iam, CfnOutput, Stack, StackProps } from 'aws-cdk-lib'
import { Construct } from 'constructs'

export class UsersStack extends Stack {
  readonly userWithPermissions: iam.IUser
  readonly userWithoutPermissions: iam.IUser
  readonly roleWithPermissions: iam.IRole

  constructor(scope: Construct, id: string, props?: StackProps) {
    super(scope, id, props)

    this.userWithPermissions = this.createUserWithPermissions()
    this.userWithoutPermissions = this.createUserWithoutPermissions()
    this.roleWithPermissions = this.createRoleWithPermissions()
  }

  private createUserWithPermissions(): iam.IUser {
    // Create user
    const user = new iam.User(this, 'UserWithPermissions', {
      userName: 'sigv4-UserWithPermissions',
    })

    user.addManagedPolicy({
      managedPolicyArn: 'arn:aws:iam::aws:policy/AmazonAPIGatewayInvokeFullAccess',
    })

    // Create access key
    const accessKey = new iam.CfnAccessKey(this, 'UserWithPermissionsAccessKey', {
      userName: user.userName,
    })

    // Create outputs
    new CfnOutput(this, 'UserWithPermissionsAccessKeyId', {
      value: accessKey.ref,
    })

    new CfnOutput(this, 'UserWithPermissionsSecretAccessKey', {
      value: accessKey.attrSecretAccessKey,
    })

    return user
  }

  private createUserWithoutPermissions(): iam.IUser {
    // Create user
    const user = new iam.User(this, 'UserWithoutPermissions', {
      userName: 'sigv4-UserWithoutPermissions',
    })

    // Create access key
    const accessKey = new iam.CfnAccessKey(this, 'UserWithoutPermissionsAccessKey', {
      userName: user.userName,
    })

    // Create outputs
    new CfnOutput(this, 'UserWithoutPermissionsAccessKeyId', {
      value: accessKey.ref,
    })

    new CfnOutput(this, 'UserWithoutPermissionsSecretAccessKey', {
      value: accessKey.attrSecretAccessKey,
    })

    return user
  }

  private createRoleWithPermissions(): iam.IRole {
    const role = new iam.Role(this, 'ApiGatewayRole', {
      assumedBy: this.userWithoutPermissions,
      roleName: 'sigv4-ApiGatewayInvoke',
    })

    role.addToPolicy(
      new iam.PolicyStatement({
        actions: ['execute-api:Invoke', 'execute-api:ManageConnections'],
        resources: ['arn:aws:execute-api:*:*:*'],
      })
    )

    // Create outputs
    new CfnOutput(this, 'ApiGatewayRoleArn', {
      value: role.roleArn,
    })

    return role
  }
}
