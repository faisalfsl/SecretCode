using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CocosSharp;

namespace CocosSharpGame1
{
    class candy : CCNode 
    {
       public  CCSprite c;
       public bool candyfalse = false;

      public  float miny;
      public  float maxy;
      public  float cminx;
      public  float cmaxx;
      public candy()
        {


           
           
            c = new CCSprite("candy1.png");
         
            //cminx=c.BoundingBoxTransformedToParent.MinX;
            //cmaxx=c.BoundingBoxTransformedToParent.MaxX;
            //   miny = c.BoundingBoxTransformedToParent.MinY;
            //   maxy = c.BoundingBoxTransformedToParent.MaxY;
            this.AddChild(c);

        }


    }
}