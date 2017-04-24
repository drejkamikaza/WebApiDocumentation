using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;
using System.Collections.Generic;

namespace WebApiDocumentation.CoreSwagger.Swagger
{
    internal class IncludeApiVersioningFilter : Swashbuckle.SwaggerGen.Generator.IOperationFilter
    {
        public void Apply(Swashbuckle.Swagger.Model.Operation operation, OperationFilterContext context)
        {

            var apiVersionParam = new NonBodyParameter
            {
                In = "header",
                Name = "api-version",
                Type = "string",
                Description = "Version header parameter",
                Required = false,
                Default = "1.0"
            };
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();
            operation.Parameters.Add(apiVersionParam);
        }
    }
}
