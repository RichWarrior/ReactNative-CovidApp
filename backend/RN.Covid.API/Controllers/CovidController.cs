using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using RN.Covid.API.Models;
using RN.Covid.API.RNGraphql;
using System;
using System.Linq;
using System.Net;

namespace RN.Covid.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CovidController : ControllerBase
    {
        readonly IDocumentExecuter _documentExecuter;
        readonly ISchema _schema;
        public CovidController(IDocumentExecuter documentExecuter, ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        /// <summary>
        /// Covid19 Bilgilerini Almak İçin Kullanılır.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index([FromBody] GraphqlQueryParameter query)
        {
            BaseResult baseResult = new BaseResult();
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = query.Variables?.ToInputs()
            };

            try
            {
                var result = _documentExecuter.ExecuteAsync(executionOptions).Result;

                if (result.Errors?.Count > 0)
                {
                    baseResult.Messages = result.Errors.Select(x => x.InnerException.Message).ToList();
                    baseResult.Status = HttpStatusCode.NotFound;
                    return new NotFoundObjectResult(baseResult);
                }

                baseResult.Data = result.Data;                
            }
            catch (Exception ex)
            {
                baseResult.Messages.Add(ex.Message);
                baseResult.Status = HttpStatusCode.NotFound;
                return new NotFoundObjectResult(baseResult);
            }
            return new JsonResult(baseResult);
        }
    }
}
