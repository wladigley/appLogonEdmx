//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogonWEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publicacao
    {
        public int Id_Publicacao { get; set; }
        public string TextPub { get; set; }
        public byte[] ImgPub { get; set; }
        public bool Privacidade { get; set; }
        public System.DateTime DataCadastro { get; set; }
        public Nullable<System.DateTime> DataUltAlteracao { get; set; }
        public int Id_Usuario { get; set; }
        public Nullable<int> Id_Comentarios { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}
