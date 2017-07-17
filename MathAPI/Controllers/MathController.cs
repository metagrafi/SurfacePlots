using MathAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Derivation;

namespace MathAPI.Controllers
{
    public class ExpressionModel
    {
        public string Formulae { get; set; }
        public List<string> Variables { get; set; }
    }
    [CorsPolicy]
    public class MathController : ApiController
    {

        [HttpPost]
        public async Task<List<string>> PostFix(ExpressionModel expression)
        {
          
            var exprTreeRoot = await Task.Run(() =>
            {
                var expressionTreeBuilder = new ExpressionTreeBuilder(expression.Formulae, expression.Variables);
                expressionTreeBuilder.Build();
                return expressionTreeBuilder.ExpressionTreeRoot;
            });
            var T = new BinaryStorage();
            T.PostOrder(exprTreeRoot);
           return T.PostFix;
        }
    }
}
