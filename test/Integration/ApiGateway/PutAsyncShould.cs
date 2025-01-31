﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AwsSignatureVersion4.Integration.ApiGateway.Authentication;
using AwsSignatureVersion4.Integration.ApiGateway.Contents;
using AwsSignatureVersion4.Private;
using Shouldly;
using Xunit;

namespace AwsSignatureVersion4.Integration.ApiGateway
{
    public class PutAsyncShould : ApiGatewayIntegrationBase
    {
        public PutAsyncShould(IntegrationTestContext context)
            : base(context)
        {
        }

        #region PutAsync(string, HttpContent, string, string, <credentials>)

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestStringAndMutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl,
                contentType.ToJsonContent(),
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestStringAndImmutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl,
                contentType.ToJsonContent(),
                Context.RegionName,
                Context.ServiceName,
                ResolveImmutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        #endregion

        #region PutAsync(Uri, HttpContent, string, string, <credentials>)

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestUriAndMutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl.ToUri(),
                contentType.ToJsonContent(),
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestUriAndImmutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl.ToUri(),
                contentType.ToJsonContent(),
                Context.RegionName,
                Context.ServiceName,
                ResolveImmutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        #endregion

        #region PutAsync(string, HttpContent, CancellationToken, string, string, <credentials>)

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestStringAndCancellationTokenAndMutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var ct = new CancellationToken();

            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl,
                contentType.ToJsonContent(),
                ct,
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestStringAndCancellationTokenAndImmutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var ct = new CancellationToken();

            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl,
                contentType.ToJsonContent(),
                ct,
                Context.RegionName,
                Context.ServiceName,
                ResolveImmutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenCancellationToken(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var ct = new CancellationToken();

            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl,
                contentType.ToJsonContent(),
                ct,
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public void AbortGivenCanceled(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var ct = new CancellationToken(true);

            // Act
            var task = HttpClient.PutAsync(
                Context.ApiGatewayUrl,
                contentType.ToJsonContent(),
                ct,
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            task.Status.ShouldBe(TaskStatus.Canceled);
        }

        #endregion

        #region PutAsync(Uri, HttpContent, CancellationToken, string, string, <credentials>)

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestUriAndCancellationTokenAndMutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var ct = new CancellationToken();

            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl.ToUri(),
                contentType.ToJsonContent(),
                ct,
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenRequestUriAndCancellationTokenAndImmutableCredentials(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var ct = new CancellationToken();

            // Act
            var response = await HttpClient.PutAsync(
                Context.ApiGatewayUrl.ToUri(),
                contentType.ToJsonContent(),
                ct,
                Context.RegionName,
                Context.ServiceName,
                ResolveImmutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        #endregion

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenQuery(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var uriBuilder = new UriBuilder(Context.ApiGatewayUrl)
            {
                Query = "Param1=value1"
            };

            // Act
            var response = await HttpClient.PutAsync(
                uriBuilder.Uri,
                contentType.ToJsonContent(),
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenOrderedQuery(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var uriBuilder = new UriBuilder(Context.ApiGatewayUrl)
            {
                Query = "Param1=Value1&Param1=value2"
            };

            // Act
            var response = await HttpClient.PutAsync(
                uriBuilder.Uri,
                contentType.ToJsonContent(),
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(IamAuthenticationType.User, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.User, typeof(RichContent))]
        [InlineData(IamAuthenticationType.Role, typeof(EmptyContent))]
        [InlineData(IamAuthenticationType.Role, typeof(RichContent))]
        public async Task SucceedGivenUnorderedQuery(IamAuthenticationType iamAuthenticationType, Type contentType)
        {
            // Arrange
            var uriBuilder = new UriBuilder(Context.ApiGatewayUrl)
            {
                Query = "Param1=value2&Param1=Value1"
            };

            // Act
            var response = await HttpClient.PutAsync(
                uriBuilder.Uri,
                contentType.ToJsonContent(),
                Context.RegionName,
                Context.ServiceName,
                ResolveMutableCredentials(iamAuthenticationType));

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
