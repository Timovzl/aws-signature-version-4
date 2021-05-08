import { aws_apigateway as apiGateway, aws_lambda as lambda, CfnOutput, Stack, StackProps } from 'aws-cdk-lib'
import { Construct } from 'constructs'

export class ApiGatewayStack extends Stack {
  constructor(scope: Construct, id: string, props?: StackProps) {
    super(scope, id, props)

    // Create Lambda
    const requestHandler = new lambda.Function(this, 'ApiRequestHandler', {
      code: lambda.Code.fromAsset(`${__dirname}/handlers`),
      handler: 'request.handler',
      runtime: lambda.Runtime.NODEJS_14_X,
    })

    // Create API Gateway
    const api = new apiGateway.LambdaRestApi(this, 'Api', {
      restApiName: 'SignatureVersion4',
      description: 'REST API endpoint for Signature Version 4 tests',
      handler: requestHandler,
      proxy: false,
    })

    api.root.addMethod('ANY', undefined, {
      authorizationType: apiGateway.AuthorizationType.IAM,
    })

    api.root.addResource('{proxy+}').addMethod('ANY', undefined, {
      authorizationType: apiGateway.AuthorizationType.IAM,
    })

    new CfnOutput(this, 'ApiGatewayUrl', {
      value: api.url.endsWith('/')
        ? api.url.slice(0, -1) // Remove the trailing slash
        : api.url,
    })
  }
}
