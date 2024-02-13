using Shooping.models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shooping.Models
{
    public class CartItem
    {

     
        public int Id { get; set; }
        public int Quantity { get; set; }

        // Foreign key for the related product
        public int ProductId { get; set; }


    }
}
