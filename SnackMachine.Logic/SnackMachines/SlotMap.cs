﻿using FluentNHibernate.Mapping;

namespace SnackMachine.Logic.SnackMachines
{
    public class SlotMap: ClassMap<Slot>
    {
        public SlotMap()
        {
            Id(x => x.Id);
            Map(x => x.Position);

            Component(x => x.SnackPile, y =>
            {
                y.Map(x => x.Quantity);
                y.Map(x => x.Price);
                y.References(x => x.Snack).Not.LazyLoad();
            });
        }
    }
}
