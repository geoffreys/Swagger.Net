﻿using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.UI;
using Swagger.Net.Factories;

namespace Swagger.Net
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : ApiController
    {
        #region --- fields & ctors ---

        private readonly ApiFactory _apiFactory;
        private readonly ResourceListingFactory _resourceListingFactory;

        public DocsController()
        {
            _resourceListingFactory = new ResourceListingFactory();
            _apiFactory = new ApiFactory();

        }

        public DocsController(ResourceListingFactory resourceListingFactory, ApiFactory apiFactory)
        {
            _resourceListingFactory = resourceListingFactory;
            _apiFactory = apiFactory;
        }

        #endregion --- fields & ctors ---

        /// <summary>
        /// Get the list of resource descriptions (Models.ResourceListing) of the api for swagger documentation
        /// </summary>
        /// <remarks>
        /// It is very convenient to have this information available for generating clients. This is the entry point for the swagger UI
        /// </remarks>
        /// <returns>JSON document that lists resource urls and descriptions </returns>
        [OutputCache(Duration = int.MaxValue, Location = OutputCacheLocation.Server)]
        public HttpResponseMessage Get()
        {
            // Arrange
            var uri = base.ControllerContext.Request.RequestUri;

            // Act
            var resourceListing = _resourceListingFactory.CreateResourceListing(uri);

            //Answer
            var resp = WrapResponse(resourceListing);
            return resp;
        }
        [OutputCache(Duration = int.MaxValue, Location = OutputCacheLocation.Server, VaryByParam = "id")]
        public HttpResponseMessage Get(string id)
        {
            // Arrange
            var rootUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            HttpResponseMessage rtnMessage;

            // Act
            if (id.ToLower() == "all")
            {
                var apis = _apiFactory.CreateAllApiDeclarations(rootUrl);
                return WrapResponse(apis);
            }
            else if (id.ToLower() == "custom")
            {
                return GetCustomMeta();
            }

            var docs = _apiFactory.CreateApiDeclaration(rootUrl, id);
            return WrapResponse(docs);
        }

        private HttpResponseMessage GetCustomMeta()
        {
            var apiDescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions;
            //var factory = new MetadataFactory(apiDescriptions);
            //var rtn = factory.CreateMetadata();
            return WrapResponse("Not implemented");
        }



        private HttpResponseMessage WrapResponse<T>(T resourceListing)
        {
            var formatter = ControllerContext.Configuration.Formatters.JsonFormatter;
            var content = new ObjectContent<T>(resourceListing, formatter);

            var response = new HttpResponseMessage { Content = content };
            return response;
        }

    }
}