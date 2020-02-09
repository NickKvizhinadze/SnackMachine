﻿using FluentNHibernate.Mapping;

namespace SnackMachine.Logic
{
    public class SnackMap: ClassMap<Snack>
    {
        public SnackMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
