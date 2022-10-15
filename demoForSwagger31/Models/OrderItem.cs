using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace demoForSwagger31.Models
{
    /// <summary>
    /// 订单项
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 订单名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>   
        /// 是否完结    
        /// </summary>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}
