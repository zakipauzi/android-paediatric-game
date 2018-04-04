using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    //config
    public int config_textScrollSpeed;

    //interface
    public UnityEngine.UI.Text ui_name;
    public UnityEngine.UI.Text ui_content;
    public UnityEngine.UI.Image ui_portrait;

    //external variables
    public int nextSceneID;
    public GameObject[] actors;

    //internal variables
    private int click_counter = 0; //counts how far through cutscene the player is)
    private Action[] cutscene;

    private bool textScrolling = false;
    private string totaltext;
    private int scrollindex;
    private int tcounter = 0;

    public int timeout = 0;
    public int clickdelay = 0;

    public AudioClip[] clips;
    public AudioSource ads;


    void Start()
    {
        Action.sc = this;
        Cutscenes.actors = actors;
        Response.ui_name = ui_name;
        Response.ui_portrait = ui_portrait;
        ads = GetComponent<AudioSource>();

        cutscene = Cutscenes.getScene(nextSceneID);

        cutscene[0].Act();
    }

    void Update()
    {
        if (timeout > 0) { timeout--; }
        if (clickdelay > -1) { clickdelay--; } 
        if (clickdelay == 0) { OnMouseDown(); }
        if (textScrolling)
        {
            tcounter++;
            if (tcounter > config_textScrollSpeed)
            {
                tcounter = 0;
                if (scrollindex < totaltext.Length)
                {
                    scrollindex++;
                    ui_content.text = totaltext.Substring(0, scrollindex);
                }
                else
                {
                    textScrolling = false;
                    tcounter = 0;
                }
            }
        }
    }


    public void OnMouseDown()
    {
        if (textScrolling == false && timeout <= 0)
        {
            click_counter++;
            if (click_counter < cutscene.Length)
            {
                cutscene[click_counter].Act();
            }
            else
            {
                Application.LoadLevel(nextSceneID);
            }
        }
        else
        {
            
        }
    }

    public void InitiateTextScrolling(string t)
    {
        textScrolling = true;
        totaltext = t;
        scrollindex = 0;
    }


}

abstract class Action
{
    public static SceneController sc;
    public abstract void Act(); //start an action   
}

//reads text out of textbox
class Response : Action
{
    public static UnityEngine.UI.Text ui_name;
    public static UnityEngine.UI.Image ui_portrait;

    private string name;
    private Sprite portrait;
    private string content;

    public Response(string name, Sprite portrait, string content)
    {
        this.name = name;
        //this.portrait = portrait;
        this.content = content;
    }

    override public void Act() //start text scrolling
    {
        ui_name.text = name;
        //ui_portrait.sprite = portrait;
        sc.InitiateTextScrolling(content);
    }

}

//activates a unity animation as configured in the animationcontroller associated with the attatched gameobject
class Move : Action
{
    Animator a;
    int t;
    public Move(Animator a, int trigger)
    {
        this.a = a;
        t = trigger;
    }

    override public void Act()
    {
        a.SetInteger("State", t);
    }
}

//prevents any action for n frames
class Wait : Action
{
    int frames;
    public Wait(int frames)
    {
        this.frames = frames;
    }

    override public void Act()
    {
        sc.timeout = frames;
    }
}

//simulates a click after n frames
class DelayClick : Action
{
    int frames;
    public DelayClick(int frames)
    {
        this.frames = frames;
    }

    override public void Act()
    {
        sc.clickdelay = frames;
    }
}

//activates a gameobject
class Activate : Action
{
    GameObject g;
    bool tf;
    public Activate(GameObject g, bool tf)
    {
        this.g = g;
        this.tf = tf;
    }

    override public void Act()
    {
        g.SetActive(tf);
    }         

}

//plays a sound
class PlaySound : Action
{
    int index;
    public PlaySound(int index)
    {
        this.index = index;
    }

    override public void Act()
    {       
        sc.ads.PlayOneShot(sc.clips[index]);
    }
    
}

//allows many animations and changing text simultaneously
class Multi : Action
{
    Action[] actions;
    public Multi(Action[] actions)
    {
        this.actions = actions;
    }

    override public void Act()
    {
        foreach (Action a in actions)
        {
            a.Act();
        }
    }
}



    static class Cutscenes
{
    public static GameObject[] actors;
    public static Action[] getScene(int i)
    {
        switch (i)
        {
            case 3:
                Animator mom = actors[0].GetComponent<Animator>();
                Animator child = actors[1].GetComponent<Animator>();
                Animator consult = actors[2].GetComponent<Animator>();
                Animator radiog = actors[3].GetComponent<Animator>();

                return new Action[] {
            new Multi(new Action[] {new Move(mom,1),new Move(child,1), new Response("Parent",null,"Hello, we are here for my child's scan."),new PlaySound(6) }),
            new Multi(new Action[] {new Response("Child",null,"Hello!"),new Move(child,2),new PlaySound(7)  }),
            new Multi(new Action[] {new Response("Consultant",null,"Welcome to the nuclear medicine department." ),new Move(consult,1),new PlaySound(0) }),
            new Multi(new Action[] {new Response("Consultant",null,"Please take a seat, someone will be with you shortly."),new Move(consult,2),new PlaySound(1) }),
            new Multi(new Action[] {new Response("Parent",null,"Okay"),new Move(child,3),new Move(mom,2),new PlaySound(5) }),
            new Multi(new Action[] {new Activate(actors[4],true),new Wait(120),new DelayClick(121) }),
            new Multi(new Action[] {new Move(radiog,1), new Response("Radiographer",null,"Hello, I am one of the Radiographers who will be helping you today"),new PlaySound(2) }),
            new Multi(new Action[] {new Move(radiog,2),new Response("Radiographer",null,"Please follow the green lights to the scanning department."),new PlaySound(3) }),
            new Multi(new Action[] {new Activate(actors[5],true),new DelayClick(121) })
        };
                
            case 5:
                Animator s2_mum = actors[0].GetComponent<Animator>();
                Animator s2_child = actors[1].GetComponent<Animator>();
                Animator s2_super = actors[2].GetComponent<Animator>();
                Animator s2_radiog = actors[3].GetComponent<Animator>();

                return new Action[] {
            new Multi(new Action[] {new Response("Radiographer",null,"Here we are in the injection room"),new PlaySound(0) }),
            new Multi(new Action[] {new Response("Radiographer",null,"This is the superintendant radiographer, he will be overseeing your scan."),new Move(s2_radiog,1),new PlaySound(1) }),
            new Multi(new Action[] {new Response("Superintendant",null,"Hello."), new Move(s2_super,1),new PlaySound(3) }),
            new Multi(new Action[] {new Response("Parent",null,"Hi, nice to meet you."),new Move(s2_mum,1),new PlaySound(2) }),
            new Multi(new Action[] {new Response("Superintendant",null,"First we will start by removing any metal items from you, because metal stops the scan from working."),new Move(s2_super,2),new PlaySound(4) }),
            new Multi(new Action[] {new Response("Radiographer",null,"Don't worry. You will get them back later!"),new Move(s2_radiog,2),new PlaySound(0) }),
            new Multi(new Action[] {new Response("Superintendant",null,"Lets begin."),new DelayClick(121),new PlaySound(5) })

};
		   case 7:
                Animator s3_mum = actors[0].GetComponent<Animator>();
                Animator s3_child = actors[1].GetComponent<Animator>();
                Animator s3_super = actors[2].GetComponent<Animator>();
                Animator s3_assist = actors[3].GetComponent<Animator>();

                return new Action[] {
                new Multi(new Action[] {new Response("Superintendant",null,"Great, now that we have removed metal objects we can proceed."),new PlaySound(0) }),
                new Multi(new Action[] {new Response("Superintendant",null,"This is my assistant, who will be doing the injection."),new Move(s3_super,1),new PlaySound(1) }),
                new Multi(new Action[] {new Response("Assistant",null,"We will put some cream on your shoulder and then we will give you the injection."),new Move(s3_assist,1),new PlaySound(5) }),
                new Multi(new Action[] {new Response("Child",null,"I-I'm scared..."),new Move(s3_child, 1),new PlaySound(3) }),
                new Multi(new Action[] {new Response("Parent",null,"Will it hurt at all doctor?"),  new Move(s3_mum, 1),new PlaySound(4) }),
                new Multi(new Action[] {new Response("Assistant",null,"Not at all! That is what the cream is for."),new Move(s3_assist,2),new PlaySound(6) }),
                new Multi(new Action[] {new Response("Superintendant",null,"Now let's get started."),new Move(s3_super,2),new PlaySound(2) })
            };

			case 9:
                Animator s4_mum = actors[0].GetComponent<Animator>();
                Animator s4_child = actors[1].GetComponent<Animator>();
                Animator s4_super = actors[2].GetComponent<Animator>();
                Animator s4_assist = actors[3].GetComponent<Animator>();
                Animator s4_cam= actors[4].GetComponent<Animator>();
                return new Action[] {
                new Multi(new Action[] {new Response("Assistant",null,"Well done! You were very brave."),new PlaySound(0) }),
                new Multi(new Action[] {new Response("Child",null,"That tickled!"),new Move(s4_child,1),new PlaySound(2) }),
                new Multi(new Action[] {new Response("Parent",null,"So what does the injection do?"),new Move(s4_mum,1),new PlaySound(3) }),
                new Multi(new Action[] {new Response("Assistant",null,"We injected a tracer which will let the scanning machine look inside their body"),new Move(s4_assist,1),new PlaySound(1) }),
                new Multi(new Action[] {new Response("Superintendant",null,"But first we will have to wait because the tracer must go around the body for a while"),new Move(s4_super,1),new PlaySound(4) }),
                new Multi(new Action[] {new Move(s4_cam,1),new DelayClick(141) })
            };

            case 11:
                Animator s5_mum = actors[0].GetComponent<Animator>();
                Animator s5_child = actors[1].GetComponent<Animator>();
                Animator s5_super = actors[2].GetComponent<Animator>();
                Animator s5_assist = actors[3].GetComponent<Animator>();
                Animator s5_radog = actors[4].GetComponent<Animator>();
                return new Action[] {
                 new Multi(new Action[] {new Response("Superintendant",null,"Here we are in the scan room."),new PlaySound(0) }),
                new Multi(new Action[] { new Response("Radiographer",null,"Hello again."),new Move(s5_radog,1),new PlaySound(1) }),
                new Multi(new Action[] {new Response("Assistant",null,"This is the machine that will be used for the scan."),new Move(s5_assist,1),new PlaySound(2) }),
                new Multi(new Action[] {new Response("Child",null,"Wow, it's big!"),new Move(s5_child,1),new PlaySound(3) }),
                new Multi(new Action[] {new Response("Assistant",null,"The scan takes a long time so you get to watch a movie or cartoons."),new Move(s5_assist,2),new PlaySound(4) }),
                new Multi(new Action[] {new Response("Child",null,"Yay!"),new Move(s5_child,2),new PlaySound(3) }),
                new Multi(new Action[] {new Response("Superintendant",null,"Try to stay still while you watch it so we can get a good scan"),new Move(s5_super,5),new PlaySound(0) }),
                new Multi(new Action[] {new Response("Assistant",null,"So which movie would you like to watch?"),new Move(s5_assist,3),new PlaySound(2) }),
            };

            case 14:
                Animator s6_mum = actors[0].GetComponent<Animator>();
                Animator s6_child = actors[1].GetComponent<Animator>();
                Animator s6_radog = actors[2].GetComponent<Animator>();
                Animator s6_childlaying = actors[3].GetComponent<Animator>();

                return new Action[] {
                new Multi(new Action[] {new Response("Assistant",null,"The scan is over. Well done!"),new PlaySound(0) }),
                new Multi(new Action[] {new Response("Child",null,"That movie was cool!"),new Activate(actors[1],true),new Activate(actors[3],false),new PlaySound(1) }),
                new Multi(new Action[] {new Response("Parent",null,"So doctor, when do we see the scan?"),new Move(s6_mum,1),new PlaySound(2) }),
                new Multi(new Action[] {new Response("Radiographer",null,"We can have a look on the screen here."),new Move(s6_radog,1),new PlaySound(3) })
            };

            case 16:
                Animator s7_mum = actors[0].GetComponent<Animator>();
                Animator s7_child = actors[1].GetComponent<Animator>();
                Animator s7_radog = actors[2].GetComponent<Animator>();
                Animator s7_assist = actors[3].GetComponent<Animator>();
                return new Action[] {
                new Multi(new Action[] {new Response("Radiographer",null,"Here we are in the hot waiting room."),new PlaySound(5) }),
                new Multi(new Action[] {new Response("Radiographer",null,"This is our healthcare assistant who will guide you through the next steps."),new Move(s7_radog,1),new PlaySound(6) }),
                new Multi(new Action[] {new Response("Assistant",null,"Hi there!"),new Move(s7_assist,1),new Move(s7_radog,2),new PlaySound(3) }),
                new Multi(new Action[] {new Response("Child",null,"Hiya!"),new Move(s7_child,1),new PlaySound(0) }),
                new Multi(new Action[] {new Response("Parent",null,"It doesn't feel very warm in here for a room called the 'hot waiting room'."),new Move(s7_mum,1),new PlaySound(1) }),
                new Multi(new Action[] {new Response("Assistant",null,"This is called the hot waiting room because the patients are here to remove the radioactive tracer."),new Move(s7_assist,2),new PlaySound(4) }),
                new Multi(new Action[] {new Response("Parent",null,"How will you do that?"),new Move(s7_mum,2),new PlaySound(2) }),
                new Multi(new Action[] {new Response("Assistant",null,"It's quite simple, the tracer leaves the body when you go to the bathroom."),new Move(s7_assist,3),new PlaySound(6) }),
                new Multi(new Action[] {new Response("Assistant",null,"So make sure to drink lots of water so you can get it out faster."),new Move(s7_assist,4),new PlaySound(5) }),
            };

            case 18:
                Animator s8_mum = actors[0].GetComponent<Animator>();
                Animator s8_child = actors[1].GetComponent<Animator>();
                Animator s8_con1 = actors[2].GetComponent<Animator>();
                Animator s8_con2 = actors[3].GetComponent<Animator>();
                return new Action[] {
                new Multi(new Action[] {new Response("Consultant",null,"With the tracer gone, that ends our appointment"),new PlaySound(0) }),
                new Multi(new Action[] {new Response("Consultant",null,"I hope you've had a good experience"),new Move(s8_con2,1),new PlaySound(2) }),
                new Multi(new Action[] { new Response("Parent",null,"Let's go arrange the next appointment."),new Move(s8_mum,1),new PlaySound(5) }),
                new Multi(new Action[] {new Response("Consultant",null,"You've been a good patient! Have a sticker."),new Move(s8_con1,1),new PlaySound(1) }),
                new Multi(new Action[] {new Response("Kid",null,"Yay!"),new Move(s8_child,1),new PlaySound(4) }),
                new Multi(new Action[] {new Response("Consultant",null,"Now you're ready for the real thing!"),new Move(s8_con2,2),new PlaySound(3) })
            };

            default: return new Action[] {new Response("Developer",null,"You shouldn't be seeing this"),};
        }
    }
}
