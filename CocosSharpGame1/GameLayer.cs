using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CocosSharpGame1
{
    public class GameLayer : CCLayer
    {

        // Define a label variable
        CCLabel RemainingCandy;
        CCNode ballsprite;
        CCSprite stick;
        CCSprite sp;
        CCSprite bg;
        float height, width;
        // int locationx = 100;
        int ly = 0;
        //  CCSprite candy;
        CCSprite candy1;
        float locationx = 50;
        candy c;
        List<CCNode> st = new List<CCNode>();
        List<CCNode> st1 = new List<CCNode>();
        CCLabel Score;
        CCLabel candycount1;
        int cc;
        int Scorecount = 0;
        int tag;

        void setcandy(CCRect position)
        {
            var locx = position.MinX + 40;
            var locy = position.MaxY - 80;


            int count = 0;
            for (int i = 0; i < 16; i++)
            {

                if (i <= 5)
                {
                    c = new CocosSharpGame1.candy();
                    c.Tag = 1 + i;
                    st.Add(c);
                    st[i].PositionX = locx;

                    st[i].PositionY = locy;

                    locx += 90;

                    this.AddChild(st[i]);

                    if (i == 5)
                    {
                        locx = 40;
                        locy -= 150;
                    }

                }
                else if (i <= 10)
                {

                    c = new CocosSharpGame1.candy();
                    c.Tag = i;
                    st.Add(c);
                    st[i].PositionX = locx;

                    st[i].PositionY = locy;

                    locx += 100;
                    this.AddChild(st[i]);
                    if (i == 10)
                    {
                        locx = 40;
                        locy -= 250;
                    }

                }

                else if (i < 30)
                {
                    ly = 300;

                    c = new CocosSharpGame1.candy();
                    c.Tag = i;
                    st.Add(c);
                    locationx = 100;

                    st[i].PositionX = locx;

                    st[i].PositionY = locy;

                    locx += 100;
                    this.AddChild(st[i]);



                }

            }


        }





        public GameLayer()
        {

            bg = new CCSprite("newbg.gif");


            RemainingCandy = new CCLabel("0", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            ballsprite = new CCSprite("ball.png");
            stick = new CCSprite("stick.png");
            
            sp = new CCSprite("stick.png");
          //  RemainingCandy.Text = this.st.Count.ToString();

            candycount1 = new CCLabel("=" + st.Count.ToString(), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);

            Score = new CCLabel("Remaing Candy", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            AddChild(bg);
            AddChild(RemainingCandy);
            AddChild(ballsprite);
            AddChild(stick);
            AddChild(Score);
            AddChild(candycount1);

            //  setcandy();



        }

        protected override void AddedToScene()
        {
            base.AddedToScene();


           // Score = st.Count;
            var bounds = VisibleBoundsWorldspace;

            CCRect Position = bounds;
            setcandy(Position);



            RemainingCandy.PositionX = bounds.MinX + 20;
            RemainingCandy.PositionY = bounds.MaxY - 15;
            ballsprite.Position = bounds.Center;
            stick.PositionX = 100;
            stick.PositionY = 100;
            bg.Position = bounds.Center;
            sp.PositionX = 100;
            sp.PositionY = 500;

            bg.ContentSize = new CCSize(1000, 1000);

            Score.PositionX = bounds.MinX + 100;
            Score.PositionY = bounds.MaxY - 15;


           // Score.PositionX = bounds.MinX + 150;
           // candycount.PositionY = bounds.MaxY - 15;



            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = OnTouchesEnded;
            AddEventListener(touchListener, this);

            Schedule(rungame);
        }


        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                stick.RunAction(new CCMoveTo(.1f, new CCPoint(touches[0].Location.X, stick.PositionY)));
                // Perform touch handling here
            }


        }




        float ballxvelocity;
        float ballyvelocity;
        const float gravity = 260;


        void rungame(float framTimeInSecond)
        {
            int i = 0;


            //var bounds = c.VisibleBoundsWorldspace;
            ballyvelocity += framTimeInSecond * -gravity;
            ballsprite.PositionX += ballxvelocity * framTimeInSecond;
            ballsprite.PositionY += (ballyvelocity * framTimeInSecond);

            bool balloverlap = ballsprite.BoundingBoxTransformedToParent.IntersectsRect(stick.BoundingBoxTransformedToParent);

            foreach (var item in st)
            {
                bool candyover = (item.BoundingBoxTransformedToParent.IntersectsRect(ballsprite.BoundingBoxTransformedToParent));


                if (candyover == true)
                {
                    

                    try
                    {

                        ballxvelocity *= (float)(-0.99);
                        CCAudioEngine.SharedEngine.PlayEffect(filename: "b1.mp3");

                        this.RemoveChildByTag(item.Tag);
                        Scorecount += 10;
                        Score.Text = Scorecount.ToString();

                        RemainingCandy.Text = this.st.Count.ToString();
                         
                        this.st.Remove(item);

                        break;

                        

                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "faisal");
                    }
                       }
               
               

                bool ismoveing = ballyvelocity < 0;

                if (balloverlap && ismoveing)
                {


                    ballyvelocity *= -1f;
                    const float minxvelocity = -300;
                    const float minyvelocity = 300;

                    ballxvelocity = CCRandom.GetRandomFloat(minxvelocity, minyvelocity);
                    //  Score += 1;
                  //  label.Text = Score.ToString();
                    //     CCAudioEngine.SharedEngine.PlayEffect(filename: "b1.mp3");
                    // Console.WriteLine(ballsprite.PositionX+"iam locationx");
                }

                float ballleft = ballsprite.BoundingBoxTransformedToParent.MinX;
                float ballright = ballsprite.BoundingBoxTransformedToParent.MaxX;
                float balltop = ballsprite.BoundingBoxTransformedToParent.MaxY;


                float screenleft = VisibleBoundsWorldspace.MinX;
                float screenright = VisibleBoundsWorldspace.MaxX;
                float screentop = VisibleBoundsWorldspace.MaxY;
                bool shouldreflaxvelocity = (ballright > screenright && ballxvelocity > 0) ||
                    (ballleft < screenleft && ballxvelocity < 0);
                bool souldreflaxvelcitytop = (balltop > screentop && ballyvelocity > 0);


                if (shouldreflaxvelocity)
                {
                    ballxvelocity *= -1;


                }

                if (souldreflaxvelcitytop)
                {
                    ballyvelocity *= -1.1f;
                    ballyvelocity /= -2.5f;
                }



                if (ballsprite.PositionY < VisibleBoundsWorldspace.MinY)
                {
                    ballsprite.PositionX = 320;
                    ballsprite.PositionY = 100;
                    ballyvelocity *= -1;
                    Scorecount = 0;
                  //  label.Text = "0";


                    
                 

                }

            }
        }

       


    }
}






