using demoForSwagger31.Enums;
using demoForSwagger31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace demoForSwagger31.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 订单是否存在
        /// </summary>
        /// <remarks>
        /// 请求示例:
        ///
        ///     GET /api/order/OrderExist/123
        ///     
        /// </remarks>
        /// <param name="id">必须可以转为Long</param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id:IsLong}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiExplorerSettings(GroupName = nameof(ApiVersion.V1))]
        public bool OrderExist([FromRoute]object id)
        {
            return true;
        }

        /// <summary>
        /// 订单最大值
        /// </summary>
        /// <param name="id">最大值20</param>
        /// <param name="linkGenerator"></param>
        /// <returns></returns>
        [HttpGet("{id:max(20)}")]
        [ApiExplorerSettings(GroupName = nameof(ApiVersion.V2))]
        public bool OrderMax(long id, [FromServices]LinkGenerator linkGenerator)
        {
            // 获取请求路径
            var actionPath = linkGenerator.GetPathByAction(HttpContext,
                action: "OrderRequest",
                controller: "Order",
                values: new { name = "abc" });

            Console.WriteLine($"ActionPath: {actionPath}");

            // 获取完整URL
            var actionUri = linkGenerator.GetUriByAction(HttpContext,
                action: "OrderRequest",
                controller: "Order",
                values: new { name = "abc" });

            Console.WriteLine($"ActionUrl: {actionUri}");

            return true;
        }

        /// <summary>
        /// 订单请求
        /// </summary>
        /// <returns></returns>
        [HttpGet("{name:required}")]
        [Obsolete]
        [ApiExplorerSettings(GroupName = nameof(ApiVersion.V3))]
        public bool OrderRequest(string name)
        {
            return true;
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        /// <param name="number">必须是三个数字</param>
        /// <returns></returns>
        [HttpGet("{number:regex(^\\d{{3}}$)}")]
        [ApiExplorerSettings(GroupName = nameof(ApiVersion.V1))]
        public bool OrderNumber(string number)
        {
            return true;
        }

        /// <summary>
        /// 获取订单项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:IsLong}")]
        [ApiExplorerSettings(GroupName = nameof(ApiVersion.V3))]
        public OrderItem GetOrderItem(long id)
        {
            return new OrderItem { Id = id };
        }
    }
}
