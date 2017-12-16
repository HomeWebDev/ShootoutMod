using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Star : MonoBehaviour
{

    public Vector3 startPostion;
    public Vector3 endPostion;
    public float AgroDistance = 5f;

    public List<TagObject> GridList = new List<TagObject>();
    public List<TagObject> OpenList = new List<TagObject>();
    public List<TagObject> CloseList = new List<TagObject>();
    public List<GameObject> Blocked = new List<GameObject>();
    private List<GameObject> FoundPath = new List<GameObject>();
    public TagObject currentNod = new TagObject();
    public TagObject goal = new TagObject();
    public TagObject start = new TagObject();
    private double F = 0.0;
    private double tentative_g_score = 0.0;
    public bool working = false;
    public float H = 1.5f;
    public float MoveCost = 1.5f;

    void Start()
    {

    }

    void Awake()
    {
        
    }


    public void CleanUp()
    {
        GridList.Clear();
        OpenList.Clear();
        CloseList.Clear();
        FoundPath.Clear();
    }

    public bool GetPathToPlayer(List<GameObject> Blocklist, Vector3 StartPosition, Vector3 EndPosition, out List<Vector3> Path)
    {
        startPostion = StartPosition;
        endPostion = EndPosition;
        if (CheckDistanceToPlayer() && working == false)
        {
            working = true;
            GridList.Clear();
            OpenList.Clear();
            CloseList.Clear();
            FoundPath.Clear();
            CreateGrid();
            Blocked = Blocklist;
            CheckifinsideBlocklist();
            CalculatePath();
            Path = BuildListPath(currentNod);
            //FoundPath = Path;
            working = false;
            return true;
        }

        Path = new List<Vector3>();
        return false;
    }

    private void CheckifinsideBlocklist()
    {
        //foreach (GameObject t in Blocked)
        //{
        //    List<GameObject> temp = new List<GameObject>();
        //    if (t.GetComponent<BoxCollider>().bounds.Contains(new Vector3(goal.X, 0f, goal.Y)))
        //    {

        //        temp.Add(t);
        //    }

        //}
    }

    public bool GetPathToPlayer(List<GameObject> obstacleList, out List<Vector3> Path)
    {
        if(CheckDistanceToPlayer())
        {
            working = true;
            GridList.Clear();
            OpenList.Clear();
            CloseList.Clear();
            Blocked.Clear();
            FoundPath.Clear();
            CreateGrid();
            CalculatePath();
            Path = BuildListPath(currentNod);
            working = false;
            //FoundPath = Path;
            return true;
        }

        Path = new List<Vector3>();
        return false;
    }

    public class TagObject : IComparable<TagObject>
    {
        public TagObject()
        {

        }

        public float X { get; set; }
        public float Y { get; set; }
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

    public bool CheckDistanceToPlayer()
    {

        float distance = Vector3.Distance(startPostion, endPostion);

        if (distance < AgroDistance)
        {
            return true;
        }
        return false;

    }

    private void CreateGrid()
    {

        start.X = (float)System.Math.Round((Decimal)startPostion.x, 0, MidpointRounding.AwayFromZero);
        start.Y = (float)System.Math.Round((Decimal)startPostion.z, 0, MidpointRounding.AwayFromZero);
        goal.X = (float)System.Math.Round((Decimal)endPostion.x, 0, MidpointRounding.AwayFromZero);
        goal.Y = (float)System.Math.Round((Decimal)endPostion.z, 0, MidpointRounding.AwayFromZero);
        //start.X = (int)startPostion.x;
        //start.Y = (int)startPostion.z;
        //goal.X = (int)endPostion.x;
        //goal.Y = (int)endPostion.z;

        //Vector3 targetDir = Player.transform.position - transform.position;
        //Vector3 forward = transform.forward; 
        //float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);

        for (float x =-AgroDistance - 1; x < AgroDistance + 1; x++)
        {
            for (float y = -AgroDistance -1; y < AgroDistance + 1; y++)
            {
                GridList.Add(new TagObject() { X = start.X + x, Y = start.Y + y });
            }
        }
    }
    public double CalculateH(TagObject curr, TagObject goal)
    {
        //return  1.5*(Math.Abs((((TagObject)goal.Tag).X - ((TagObject)curr.Tag).X)) + Math.Abs((((TagObject)goal.Tag).Y - ((TagObject)curr.Tag).Y)));
        return H * Math.Max(Math.Abs(goal.X - curr.X), Math.Abs(goal.Y - curr.Y));
    }

    public List<Vector3> BuildListPath(TagObject last)
    {
        List<Vector3> localstack = new List<Vector3>();
        while (last != null)
        {
            //var ng = new GameObject();
            //ng.transform.position = new Vector3(last.X, 0, last.Y);
            localstack.Add(new Vector3(last.X, 0, last.Y));
            last = last.Parant;
        }
        return localstack;
    }
    public void CalculatePath()
    {
        CloseList.Clear();
        OpenList.Add(start);
        start.G = 0;
        F = start.G + CalculateH(start, goal);
        while (OpenList.Count != 0)
        {
            currentNod = OpenList.OrderBy(x => x.F).First();

            if ((currentNod.X == goal.X) &&
                (currentNod.Y == goal.Y))
            {
                return;
            }
            OpenList.Remove(currentNod);
            CloseList.Add(currentNod);
            //ColorTransform(currentNod.Name, Brushes.Violet);
            foreach (TagObject taggy in GetNeigborNods(currentNod))
            {
                if (taggy != null)
                {
                    if (CloseList.Exists(x => x == taggy))
                    {
                        //if (((TagObject)r.Tag).X != ((TagObject)start.Tag).X && ((TagObject)r.Tag).Y != ((TagObject)start.Tag).Y)
                        //ColorTransform(r.Name, Brushes.Black);
                        continue;
                    }
                    tentative_g_score = currentNod.G + DistanceToNeighbor(currentNod, taggy);

                    bool b = OpenList.Exists(x => x != taggy);

                    if (!(OpenList.Exists(x => x == taggy)) || tentative_g_score < taggy.G || OpenList.Count == 0)
                    {
                        taggy.Parant = currentNod;
                        taggy.G = tentative_g_score;
                        F = taggy.G + CalculateH(taggy, goal);
                        if (!OpenList.Exists(x => x == taggy) || OpenList.Count == 0)
                        {
                            taggy.F = F;
                            //ColorTransform(r.Name, Brushes.Violet);
                            OpenList.Add(taggy);
                        }
                    }
                }
            }
        }
    }

    public double DistanceToNeighbor(TagObject curr, TagObject neighbor)
    {
        //return 1;
        if (Math.Abs(curr.X - neighbor.X) + Math.Abs(curr.Y - neighbor.Y) == 2)
            return MoveCost;
        else
            return 1;
        //return Math.Abs((((TagObject)curr.Tag).X - ((TagObject)neighbor.Tag).X)) + Math.Abs((((TagObject)curr.Tag).Y - ((TagObject)neighbor.Tag).Y));
    }


    public List<TagObject> GetNeigborNods(TagObject current)
    {
        List<TagObject> returnList = new List<TagObject>();
        TagObject temp = new TagObject();


        var checker = GridList.First() as TagObject;

        checker = GridList.Where(x => x.X.Equals(current.X)
                                                    && x.Y.Equals(current.Y + 1)
                                                    && !CheckIfBlocked(x)) as TagObject;

        temp = GridList.Find(x => x.X.Equals(current.X) && x.Y.Equals(current.Y + 1) && !CheckIfBlocked(x));
        if(temp != null)
            returnList.Add(temp);
        temp = GridList.Find(x => x.X.Equals(current.X) && x.Y.Equals(current.Y - 1) && !CheckIfBlocked(x));
        if (temp != null)
            returnList.Add(temp);
        temp = GridList.Find(x => x.X.Equals(current.X + 1) && x.Y.Equals(current.Y) && !CheckIfBlocked(x));
        if (temp != null)
            returnList.Add(temp);
        temp = GridList.Find(x => x.X.Equals(current.X - 1) && x.Y.Equals(current.Y) && !CheckIfBlocked(x));
        if (temp != null)
            returnList.Add(temp);
        temp = GridList.Find(x => x.X.Equals(current.X + 1) && x.Y.Equals(current.Y + 1) && !CheckIfBlocked(x));
        if (temp != null)
            returnList.Add(temp);
        temp = GridList.Find(x => x.X.Equals(current.X - 1) && x.Y.Equals(current.Y - 1) && !CheckIfBlocked(x));
        if (temp != null)
            returnList.Add(temp);
        temp = GridList.Find(x => x.X.Equals(current.X - 1) && x.Y.Equals(current.Y + 1) && !CheckIfBlocked(x));
        if (temp != null)
            returnList.Add(temp);
        temp = GridList.Find(x => x.X.Equals(current.X + 1) && x.Y.Equals(current.Y - 1) && !CheckIfBlocked(x));
        if (temp != null)
            returnList.Add(temp);

        return returnList;
    }

    public bool CheckIfBlocked(TagObject to)
    {

        foreach (GameObject t in Blocked)
        {
            if (t.GetComponent<BoxCollider>().bounds.Contains(new Vector3(goal.X, 0f, goal.Y)))
            {

                return false;
            }
            if (t.GetComponent<BoxCollider>().bounds.Contains(new Vector3(to.X, 0f, to.Y)))
            {
                return true;
            }
        }
        return false;
        //foreach (TagObject t in Blocked)
        //{
        //    if ((t.X == to.X) && (t.Y == to.Y))
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }
}
