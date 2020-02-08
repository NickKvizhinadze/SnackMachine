﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public class SnackPile : ValueObject<SnackPile>
    {
        #region Constructor
        public SnackPile()
        {
        }

        public SnackPile(Snack snack, decimal price, int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException();

            if (price < 0)
                throw new InvalidOperationException();

            if (price % 0.01m > 0)
                throw new InvalidOperationException();

            Snack = snack;
            Price = price;
            Quantity = quantity;
        }

        #endregion

        #region Methods
        public SnackPile SubstractOne()
        {
            return new SnackPile(Snack, Price, Quantity - 1);
        }
        #endregion

        #region Properties
        public virtual Snack Snack { get; }
        public virtual decimal Price { get; }
        public virtual int Quantity { get; }
        #endregion

        #region Protected Methods
        protected override bool EqualsCore(SnackPile other)
        {
            return Snack == other.Snack
                && Quantity == other.Quantity
                && Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Snack.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }
        #endregion
    }
}
