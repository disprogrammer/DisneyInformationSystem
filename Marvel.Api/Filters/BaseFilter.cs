using Marvel.Api.Models.Enums;

namespace Marvel.Api.Filters
{
    /// <summary>
    /// BaseFilter class.
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// Ordering results.
        /// </summary>
        private OrderResult _orderBy;

        /// <summary>
        /// Orders models by the order result.
        /// </summary>
        /// <param name="order">Order result.</param>
        public void OrderBy(OrderResult order)
        {
            if ((order & _orderBy) == 0)
                _orderBy |= order;
        }

        /// <summary>
        /// Order the result set by a field or fields
        /// </summary>
        public string ResultSetOrder
        {
            get
            {
                var result = new List<string>();
                foreach (OrderResult order in Enum.GetValues(typeof(OrderResult)))
                {
                    if ((_orderBy & order) != 0)
                        result.Add(order.GetDescription());
                }

                if (result.Count == 0)
                    return string.Empty;

                return string.Join(",", result.ToArray());
            }
        }

        /// <summary>
        /// Limit the result set to the specified number of resources.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Skip the specified number of resources in the result set.
        /// </summary>
        public int? Offset { get; set; }
    }
}