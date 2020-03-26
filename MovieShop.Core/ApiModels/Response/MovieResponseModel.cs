using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.ApiModels.Response
{
   public class MovieResponseModel
    {
        //data tranfer objects
        //in mvc this is called view models
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
       
    }
}
