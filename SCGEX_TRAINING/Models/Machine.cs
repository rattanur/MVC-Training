using System;
using System.Collections.Generic;
using System.Linq;

namespace SCGEX_TRAINING.Models
{
    public class Machine
    {

        // fields
        private decimal _totalAmount = 0m;
        private bool _isActive = true;

        // constructors
        public Machine()
        {
            //AcceptableCoins = new List<decimal> { 1, 5, 10 };
            Products = new HashSet<Product>();
            Slots = new HashSet<Slot>();
        }

        // properties
        public int Id { get; set; }
        public decimal TotalAmount { get => _totalAmount; private set => _totalAmount = value; }
        public bool IsActive { get => _isActive; private set => _isActive = value; }
        public List<decimal> AcceptableCoins { get; private set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }

        // method
        public void AcceptCoin(decimal amount)
        {
            if (!_isActive) throw new Exception("Machine is Off");
            if (amount == 1) throw new Exception("Invalid Coin");

            _totalAmount += amount;
        }

        public void ClearCoin()
        {
            _totalAmount = 0m;
        }

        public void SwitchMachineOff()
        {
            ClearCoin();
            _isActive = false;
        }

        public void SwitchMachineOn()
        {
            _isActive = true;
        }

        public void AddProducts(ProductInfo productInfo, int quantity, DateTime expiredDate)
        {
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (productInfo == null) throw new ArgumentNullException(nameof(productInfo));
            if (expiredDate < DateTime.Today) throw new ArgumentOutOfRangeException(nameof(expiredDate));

            for (int i = 0; i < quantity; i++)
            {
                var p = new Product();
                p.Machine = this;
                p.ProductInfo = productInfo;
                p.AddedDate = DateTime.Now;
                p.ExpiredDate = expiredDate;
                p.SoldDate = null;

                this.Products.Add(p);

            }

            // Update Slots
            var slot = Slots.SingleOrDefault(x => x.ProductCode == productInfo.Code);
            if (slot == null)
            {
                slot = new Slot();
                slot.ProductCode = productInfo.Code;
                slot.Quantity = quantity;
                slot.ProductName = productInfo.Name;
                slot.Price = productInfo.Price;

                Slots.Add(slot);
            }
            else
            {
                slot.Quantity += quantity;
            }

        }
    }
}