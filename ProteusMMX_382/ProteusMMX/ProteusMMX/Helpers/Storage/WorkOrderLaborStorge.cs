using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Helpers.Storage
{
    public static class WorkOrderLaborStorge
    {
        public static ISimpleStorage Storage { get { return DependencyService.Get<ISimpleStorage>(); } }

    }
    public static class WorkorderInspectionStorge
    {
        public static ISimpleStorage Storage { get { return DependencyService.Get<ISimpleStorage>(); } }

    }

    public static class SignatureStorage
    {
        public static ISimpleStorage Storage { get { return DependencyService.Get<ISimpleStorage>(); } }

    }
}
