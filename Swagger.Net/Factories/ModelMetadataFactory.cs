using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Swagger.Net.Models;

namespace Swagger.Net.Factories
{
    public class ModelMetadataFactory
    {
        #region --- fields & ctors ---

        private readonly XmlCommentDocumentationProvider _docProvider;

        public ModelMetadataFactory()
        {
            _docProvider = (XmlCommentDocumentationProvider)GlobalConfiguration.Configuration.Services.GetService((typeof(IDocumentationProvider)));
        }

        public ModelMetadataFactory(XmlCommentDocumentationProvider documentationProvider)
        {
            _docProvider = documentationProvider;
        }

        #endregion --- fields & ctors ---

        public IEnumerable<Model> GetModels(IEnumerable<ApiDescription> apiDescs)
        {
            var rtnModels = new Dictionary<Type, Model>();
            foreach (var apiDesc in apiDescs)
            {
                AddIfValid(apiDesc.ActionDescriptor.ReturnType, rtnModels);

                foreach (var param in apiDesc.ParameterDescriptions)
                {
                    AddIfValid(param.ParameterDescriptor.ParameterType, rtnModels);
                }
            }

            return rtnModels.Values;
        }

        private void AddIfValid(Type myType, Dictionary<Type, Model> rtnModels)
        {
            if (IsOfInterest(myType))
            {
                if (myType.IsGenericType)
                {
                    myType = myType.GetGenericArguments()[0];
                }
                if (!rtnModels.ContainsKey(myType))
                {
                    var model = _docProvider.GetApiModel(myType);
                    rtnModels.Add(myType, model);
                }
            }
        }

        private bool IsOfInterest(Type returnType)
        {
            if (returnType == null) return false;

            if (returnType.IsGenericType)
            {
                returnType = returnType.GetGenericArguments()[0];
            }

            if (returnType.IsPrimitive || returnType == typeof(string))
            {
                return false;
            }
            return true;
        }
   
    }
}