using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Star : MonoBehaviour
{
    public GameObject Player;
    public float AgroDistance = 100f;
    public List<int> GridList = new List<int>();
    public List<TagObject> OpenList = new List<TagObject>();
    public List<TagObject> CloseList = new List<TagObject>();
    public List<TagObject> Blocked = new List<TagObject>();
    public TagObject currentNod;
    public TagObject goal, start;


    public class TagObject : IComparable<TagObject> 
    {
        public TagObject()
        {

        }

        public double X { get; set; }
        public double Y { get; set; }
        public double F { get; set; }
        public double G { get; set; }
        public TagObject Parant { get; set; }
        public TagObject Child { get; set; }


        int IComparable<TagObject>.CompareTo(TagObject other)
        {
            double sumOther = other.F;
            double sumThis = this.F;

            if (sumOther > sumThis)
                return -1;
            else if (sumOther == sumThis)
                return 0;
            else
                return 1;
        }

    }

    public void CheckDistanceToPlayer()
    {

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < AgroDistance)
        {
            CreateGrid();
        }
    }

    private void CreateGrid()
    {
        start.X = transform.position.x;
        start.Y = transform.position.z;
        goal.X = Player.transform.position.x;
        goal.Y = Player.transform.position.z;

        Vector3 targetDir = Player.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        for (int x = 0; x < AgroDistance; x++)
        {
            for (int y = 0; y < AgroDistance; y++)
            {
                OpenList.Add(new TagObject() { X = start.X+x, Y = start.Y+y });
            }
        }
    }

    //public void CalculatePath()
    //{
    //    CloseList.Clear();
    //    OpenList.Add(start);
    //    ((TagObject)start.tag).G = 0;
    //    F = ((TagObject)start.Tag).G + CalculateH(start, goal);
    //    while (OpenList.Count != 0)
    //    {
    //        currentNod = OpenList.OrderBy(x => ((TagObject)x.Tag).F).First();

    //        if (currentNod == goal)
    //        {
    //            DrawFastLine();
    //            return;
    //        }
    //        OpenList.Remove(currentNod);
    //        CloseList.Add(currentNod);
    //        //ColorTransform(currentNod.Name, Brushes.Violet);
    //        foreach (Transform r in GetNeigborNods(currentNod))
    //        {
    //            if (r != null)
    //            {
    //                if (CloseList.Exists(x => x.Name == r.Name))
    //                {
    //                    //if (((TagObject)r.Tag).X != ((TagObject)start.Tag).X && ((TagObject)r.Tag).Y != ((TagObject)start.Tag).Y)
    //                    //ColorTransform(r.Name, Brushes.Black);
    //                    continue;
    //                }
    //                tentative_g_score = ((TagObject)currentNod.Tag).G + DistanceToNeighbor(currentNod, r);

    //                bool b = OpenList.Exists(x => x.Name != r.Name);

    //                if (!(OpenList.Exists(x => x.Name == r.Name)) || tentative_g_score < ((TagObject)r.Tag).G || OpenList.Count == 0)
    //                {
    //                    ((TagObject)r.Tag).Parant = ((TagObject)currentNod.Tag);
    //                    ((TagObject)r.Tag).G = tentative_g_score;
    //                    F = ((TagObject)r.Tag).G + CalculateH(r, goal);
    //                    if (!OpenList.Exists(x => x.Name == r.Name) || OpenList.Count == 0)
    //                    {
    //                        ((TagObject)r.Tag).F = F;
    //                        ColorTransform(r.Name, Brushes.Violet);
    //                        OpenList.Add(r);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //public double DistanceToNeighbor(Transform curr, Transform neighbor)
    //{
    //    //return 1;
    //    if (Math.Abs((((TagObject)curr.Tag).X - ((TagObject)neighbor.Tag).X)) + Math.Abs((((TagObject)curr.Tag).Y - ((TagObject)neighbor.Tag).Y)) == 2)
    //        return 1.5;
    //    else
    //        return 1;
    //    //return Math.Abs((((TagObject)curr.Tag).X - ((TagObject)neighbor.Tag).X)) + Math.Abs((((TagObject)curr.Tag).Y - ((TagObject)neighbor.Tag).Y));
    //}

    //public double CalculateH(Transform curr, Transform goal)
    //{
    //    //return  1.5*(Math.Abs((((TagObject)goal.Tag).X - ((TagObject)curr.Tag).X)) + Math.Abs((((TagObject)goal.Tag).Y - ((TagObject)curr.Tag).Y)));
    //    return 4 * Math.Max(Math.Abs((((TagObject)goal.Tag).X - ((TagObject)curr.Tag).X)), Math.Abs((((TagObject)goal.Tag).Y - ((TagObject)curr.Tag).Y)));
    //}

    //public void DrawFastLine()
    //{
    //    TagObject t = new TagObject();
    //    t = ((TagObject)currentNod.Tag);

    //    foreach (Transform c in CloseList)
    //    {
    //        ColorTransform(c.Name, Brushes.Black);
    //    }

    //    while (t != null)
    //    {
    //        ColorTransform("X" + t.X + "Y" + t.Y, Brushes.SkyBlue);
    //        path.Content = path.Content + "::" + t.X.ToString() + "," + t.Y.ToString();

    //        t = t.Parant;
    //    }
    //    //foreach (Transform r in CloseList)
    //    //{
    //    //    ColorTransform(r.Name, Brushes.Black);
    //    //}
    //    ColorTransform(CloseList.First().Name, Brushes.Gold);
    //    ColorTransform(currentNod.Name, Brushes.Red);
    //    Count.Content = "OpenList: " + OpenList.Count + " " + "CloseList: " + CloseList.Count;
    //}

    //public Transform GetTransform(string name)
    //{
    //    Transform r = (Transform)GridGUI.FindName(name);
    //    if (r != null)
    //        return r;
    //    return default(Transform);
    //}

    //public void ColorTransform(string name, SolidColorBrush color)
    //{
    //    Transform r = GetTransform(name);
    //    if (r != null)
    //        r.Fill = color;
    //    else
    //    {
    //        MessageBox.Show("No object found");
    //    }
    //}

    //public List<Transform> GetNeigborNods(Transform current)
    //{
    //    List<Transform> returnList = new List<Transform>();
    //    Transform temp = new Transform();

    //    if (((TagObject)current.Tag).X != 0.0 || ((TagObject)current.Tag).Y != 0.0)
    //    {
    //        temp = GetTransform("X" + Convert.ToString((((TagObject)current.Tag).X - 1.0)) + "Y" + Convert.ToString(((TagObject)current.Tag).Y - 1.0));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //        temp = (GetTransform("X" + Convert.ToString(((TagObject)current.Tag).X) + "Y" + Convert.ToString(((TagObject)current.Tag).Y - 1.0)));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //        temp = (GetTransform("X" + Convert.ToString((((TagObject)current.Tag).X + 1.0)) + "Y" + Convert.ToString(((TagObject)current.Tag).Y - 1.0)));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //        temp = (GetTransform("X" + Convert.ToString((((TagObject)current.Tag).X + 1.0)) + "Y" + Convert.ToString(((TagObject)current.Tag).Y)));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //        temp = (GetTransform("X" + Convert.ToString((((TagObject)current.Tag).X + 1.0)) + "Y" + Convert.ToString(((TagObject)current.Tag).Y + 1.0)));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //        temp = (GetTransform("X" + Convert.ToString((((TagObject)current.Tag).X)) + "Y" + Convert.ToString(((TagObject)current.Tag).Y + 1.0)));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //        temp = (GetTransform("X" + Convert.ToString((((TagObject)current.Tag).X - 1.0)) + "Y" + Convert.ToString(((TagObject)current.Tag).Y + 1.0)));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //        temp = (GetTransform("X" + Convert.ToString((((TagObject)current.Tag).X - 1.0)) + "Y" + Convert.ToString(((TagObject)current.Tag).Y)));
    //        if (temp != null && !CheckIfBlocked(((TagObject)temp.Tag)))
    //        {
    //            returnList.Add(temp);
    //        }
    //    }
    //    return returnList;
    //}

    //public bool CheckIfBlocked(TagObject to)
    //{
    //    foreach (Transform r in Blocked)
    //    {
    //        if ((((TagObject)r.Tag).X == to.X) && (((TagObject)r.Tag).Y == to.Y))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
}
