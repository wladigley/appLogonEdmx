using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogonWEB.Models;
namespace LogonWEB.Manager
{
    public class ImagemManager
    {

        public static string GetUsuarioImagem(Usuarios p)
        {
            var imageBase = "";
            try
            {
                
                var img = p.Imagem;

                if (img == null || img.ToArray().Equals(""))
                {

                    return imageBase = string.Format("../img/icon-default.png");
                }
                else
                {
                    var base64 = Convert.ToBase64String(img);
                    return imageBase = String.Format("data:image/gif;base64,{0}", base64);

                }
            }catch(Exception ex)
            {

                return imageBase = string.Format("../img/icon-default.png");
            }
               
            
        }



    }
}



