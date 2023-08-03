using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand : MyBehaviour
{
[SerializeField] protected List<float> thislist ;
public static float UCLN(float a, float b)
{
    while (a != b && b != 0 && a != 0)
        {
            if (a > b) a = a - b;
            else b = b - a;
        }
        return a;
}
public static float AddKey(List<float> List)
    {
        List<float> Listindex = new List<float>();
        if(Listindex.Count > 0)
        {
            Listindex.Clear();
        }

        int j = 0,dem = 0;
        for(int i = 0 ; i < List.Count; i++)
        {
           while(j < dem + (int)(ListReng(List)*(Toint(List[i])/100f)))
           {
           Listindex.Add(i);
            j++;
           } 
           dem = j;
        }
        return Randominlist(Listindex);
    }
    public static List<float> check(List<float> List)
    {
        List<float> Listindex = new List<float>();
        if(Listindex.Count > 0)
        {
            Listindex.Clear();
        }
        int j = 0,dem = 0;
        for(int i = 0 ; i < List.Count; i++)
        {
           while(j < dem + (int)(ListReng(List)*(Toint(List[i])/100f)))
           {
           Listindex.Add(i);
            j++;
           } 
           dem = j;
        }
        return Listindex;
    }

public static float Randominlist(List<float> List)
    {
       int h = Random.Range(0,List.Count);
        return List[h];
    }
public static float Toint(float h)
{
    float a = h - (int)(h);
    if( a >= 0.5f) return (int) h + 1 ;
    if(a < 0.5f) return (int) h;
    else return h;
}
    
public static List<float> Main(List<float> List)
    {   
    List<float> thislist  = new List<float>(List);    
        while(thislist.Count != 0)
        {
            thislist.Clear();
        }
        if(thislist.Count == 0)
        {
            for(int i =0;i < List.Count ;i ++)
            {
                thislist.Add(List[i]);
            }
        }
    List<float> cachelist = new List<float>();    
    List<float> result = new List<float>();
     for(int i = 0 ; i < thislist.Count ; i++) cachelist.Add(0);
        while(SumOfElements(thislist) > 100f)
         {
            float h = SumOfElements(thislist);
            for(int i = 0 ; i < thislist.Count ; i++)
             {
               cachelist[i] = (thislist[i])/(h/100f);
               thislist[i] = thislist[i] - cachelist[i];
             }
           result.Add(AddKey(cachelist));
         }
        if(SumOfElements(thislist) <= 100f)  
        {
            result.Add(AddKey(thislist));
        }
        return result;
    }
public static float ListReng(List<float> List)
{
    float max =0;   
        for (int i= 0 ; i < List.Count; i++)
            {
                if (100f/UCLN(100f,Toint(List[i])) > max){
                    max = 100f/UCLN(100f,Toint(List[i]));
                }
            }
            return max;
}
public static float SumOfElements(List<float> List)
{
    float result = 0;
    for(int i = 0 ;  i < List.Count; i++)
    {
      result += List[i];
    }
    return result;
}

}
