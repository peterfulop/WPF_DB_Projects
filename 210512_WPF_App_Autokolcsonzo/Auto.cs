//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _210512_WPF_App_Autokolcsonzo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auto
    {
        public int Id { get; set; }
        public string Rendszam { get; set; }
        public string Marka { get; set; }
        public string Tipus { get; set; }
        public string Szin { get; set; }
        public Nullable<int> Evjarat { get; set; }
    
        public virtual Kolcsonzo Kolcsonzo { get; set; }
    }
}
