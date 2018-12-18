using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateQuest : MonoBehaviour
{
    
    private WaypointManger wpm;
    
    public GameObject[] waypoints;
    public GameObject[] A1Tiles;

    public GameObject genFloor; //testing only. will be replaced with actual tiles. or will it? standard floor that gets "assets" placed on top?
    public GameObject emptyFloor;


    private int questDistance = 7;
    private int questSteps;
    private List<string> directions = new List<string>();

    private int currentLoc = 0;

    private int[] grid;
    private int tiles = 25;


    void Start()
    {
        //create grid
        grid = new int[tiles];

        //link waypoint manager
        GameObject wpmgo = GameObject.Find("[Game Manager]");
        wpm = wpmgo.GetComponent<WaypointManger>();

        
        CreatePath(); //create a 7 tile quest path
        InstantiateTiles(); //take path and create world tiles
        SendWaypointsToManger(); //send path tiles waypoints to manager
    }

#region "Create Path"
    void CreatePath()
    {
        
        //start new quest
        QuestStart();

        //loop until distance = 0
        //or break and retry
        do
        {
            if (!NextStep(currentLoc))
            {
                //unusable path. restart
                QuestStart();
                
            }

        } while (questSteps > 0);

        //now we have a quest path in "directions" list
                
    }

    void QuestStart()
    {
        //set all tiles and path to 0
        ResetGrid();
        directions.Clear();
        questSteps = questDistance;

        //set start of path
        currentLoc = Random.Range(0, tiles);
            //add start coordinates to directions
        directions.Add(currentLoc.ToString());
            //mark start coordinates on grid and decrease by 1
        grid[currentLoc] = questSteps;
        questSteps -= 1;
    }

    void ResetGrid()
    {
        for (int i = 0; i < tiles; i++)
        {
                grid[i] = 0;
        }
    }

    bool NextStep(int CL) //does the current location have a possbile next step?
    {
        switch (CL)
            {
                //Row A
                #region "A1"
                case 0: //A1 out
                if (grid[1] == 0 || grid[5] == 0 || grid[6] == 0 || grid[15] == 0) //possible next (A2, B1, B2, D1[Volcano]), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    A1Start:
                    int rand = Random.Range(0,4); //4 possible next tiles
                    switch (rand)
                    {
                        case 0: //check east to 1
                        if (grid[1] != 0) //east to 1 not available
                        {
                        goto A1Start;
                        } else { //east to 1 is available
                        //move east to 1
                        directions.Add("E");
                        directions.Add("1");
                        grid[1] = questSteps;
                        currentLoc = 1;
                        break;
                        }

                        case 1: //check south east to 6
                        if (grid[6] != 0) //south east to 6 not available
                        {
                        goto A1Start;
                        } else { //south east to 6 is available
                        //move south east to 6
                        directions.Add("SE");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }

                        case 2: //check south to 5
                        if (grid[5] != 0) //south to 5 not available
                        {
                        goto A1Start;
                        } else { //south to 5 is available
                        //move south to 5
                        directions.Add("S");
                        directions.Add("5");
                        grid[5] = questSteps;
                        currentLoc = 5;
                        break;
                        }

                        case 3: //check north to 15
                        if (grid[15] != 0) //north to 15 not available
                        {
                        goto A1Start;
                        } else { //north to 15 is available
                        //move north to 15
                        directions.Add("N");
                        directions.Add("15");
                        grid[5] = questSteps;
                        grid[10] = questSteps;
                        grid[15] = questSteps;
                        currentLoc = 15;
                        break;
                        }

                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "A2"
                case 1: //A2 out
                if (grid[0] == 0 || grid[2] == 0 || grid[5] == 0 || grid[6] == 0 || grid[7] == 0) //possible next (A3, B3, B2, B1, A1), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    A2Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check east to 2
                        if (grid[2] != 0) //east to 2 not available
                        {
                        goto A2Start;
                        } else { //east to 2 is available
                        //move east to 2
                        directions.Add("E");
                        directions.Add("2");
                        grid[2] = questSteps;
                        currentLoc = 2;
                        break;
                        }

                        case 1: //check south east to 7
                        if (grid[7] != 0) //south east to 7 not available
                        {
                        goto A2Start;
                        } else { //south east to 7 is available
                        //move south east to 7
                        directions.Add("SE");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }

                        case 2: //check south to 6
                        if (grid[6] != 0) //south to 6 not available
                        {
                        goto A2Start;
                        } else { //south to 6 is available
                        //move south to 6
                        directions.Add("S");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }

                        case 3: //check south west to 5
                        if (grid[5] != 0) //south west to 5 not available
                        {
                        goto A2Start;
                        } else { //south west to 5 is available
                        //move south west to 5
                        directions.Add("SW");
                        directions.Add("5");
                        grid[5] = questSteps;
                        currentLoc = 5;
                        break;
                        }

                        case 4: //check west to 0
                        if (grid[0] != 0) //west to 0 not available
                        {
                        goto A2Start;
                        } else { //west to 0 is available
                        //move west to 0
                        directions.Add("W");
                        directions.Add("0");
                        grid[0] = questSteps;
                        currentLoc = 0;
                        break;
                        }

                        

                    }

                    return true;

                } else{
                    return false;
                }
                #endregion

                #region "A3"
                case 2: //A3 out
                if (grid[1] == 0 || grid[3] == 0 || grid[6] == 0 || grid[7] == 0 || grid[8] == 0) //possible next (A4, B4, B3, B2, A2), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    A3Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check east to 3
                        if (grid[3] != 0) //east to 3 not available
                        {
                        goto A3Start;
                        } else { //east to 3 is available
                        //move east to 3
                        directions.Add("E");
                        directions.Add("3");
                        grid[3] = questSteps;
                        currentLoc = 3;
                        break;
                        }

                        case 1: //check south east to 8
                        if (grid[8] != 0) //south east to 8 not available
                        {
                        goto A3Start;
                        } else { //south east to 8 is available
                        //move south east to 8
                        directions.Add("SE");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }

                        case 2: //check south to 7
                        if (grid[7] != 0) //south to 7 not available
                        {
                        goto A3Start;
                        } else { //south to 7 is available
                        //move south to 7
                        directions.Add("S");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }

                        case 3: //check south west to 6
                        if (grid[6] != 0) //south west to 6 not available
                        {
                        goto A3Start;
                        } else { //south west to 6 is available
                        //move south west to 6
                        directions.Add("SW");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }

                        case 4: //check west to 1
                        if (grid[1] != 0) //west to 1 not available
                        {
                        goto A3Start;
                        } else { //west to 1 is available
                        //move west to 1
                        directions.Add("W");
                        directions.Add("1");
                        grid[1] = questSteps;
                        currentLoc = 1;
                        break;
                        }

                        

                    }

                    return true;

                } else{
                    return false;
                }
                #endregion

                #region "A4"
                case 3: //A4 out
                if (grid[2] == 0 || grid[4] == 0 || grid[7] == 0 || grid[8] == 0 || grid[9] == 0) //possible next (A5, B5, B4, B3, A3), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    A4Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check east to 4
                        if (grid[4] != 0) //east to 4 not available
                        {
                        goto A4Start;
                        } else { //east to 4 is available
                        //move east to 4
                        directions.Add("E");
                        directions.Add("4");
                        grid[4] = questSteps;
                        currentLoc = 4;
                        break;
                        }

                        case 1: //check south east to 9
                        if (grid[9] != 0) //south east to 9 not available
                        {
                        goto A4Start;
                        } else { //south east to 9 is available
                        //move south east to 9
                        directions.Add("SE");
                        directions.Add("9");
                        grid[9] = questSteps;
                        currentLoc = 9;
                        break;
                        }

                        case 2: //check south to 8
                        if (grid[8] != 0) //south to 8 not available
                        {
                        goto A4Start;
                        } else { //south to 8 is available
                        //move south to 8
                        directions.Add("S");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }

                        case 3: //check south west to 7
                        if (grid[7] != 0) //south west to 7 not available
                        {
                        goto A4Start;
                        } else { //south west to 7 is available
                        //move south west to 7
                        directions.Add("SW");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }

                        case 4: //check west to 2
                        if (grid[2] != 0) //west to 2 not available
                        {
                        goto A4Start;
                        } else { //west to 2 is available
                        //move west to 2
                        directions.Add("W");
                        directions.Add("2");
                        grid[2] = questSteps;
                        currentLoc = 2;
                        break;
                        }
                    }

                    return true;

                } else{
                    return false;
                }
                #endregion

                #region "A5"
                case 4: //A5 out
                if (grid[3] == 0 || grid[8] == 0 || grid[9] == 0) //possible next (B5, B4, A4), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    A5Start:
                    int rand = Random.Range(0,3); //3 possible next tiles
                    switch (rand)
                    {
                        case 0: //check south to 9
                        if (grid[9] != 0) //south to 9 not available
                        {
                        goto A5Start;
                        } else { //south to 9 is available
                        //move south to 9
                        directions.Add("S");
                        directions.Add("9");
                        grid[9] = questSteps;
                        currentLoc = 9;
                        break;
                        }

                        case 1: //check south west to 8
                        if (grid[8] != 0) //south west to 8 not available
                        {
                        goto A5Start;
                        } else { //south west to 8 is available
                        //move south west to 8
                        directions.Add("SW");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }

                        case 2: //check west to 3
                        if (grid[3] != 0) //west to 3 not available
                        {
                        goto A5Start;
                        } else { //west to 3 is available
                        //move west to 3
                        directions.Add("W");
                        directions.Add("3");
                        grid[3] = questSteps;
                        currentLoc = 3;
                        break;
                        }

                    }

                    return true;

                } else{
                    return false;
                }
                #endregion
                    
                //Row B
                #region "B1"
                case 5: //B1 out
                if (grid[0] == 0 || grid[1] == 0 || grid[6] == 0 || grid[10] == 0 || grid[11] == 0) //possible next (A1, A2, B2, C2, C1), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    B1Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north to 0
                        if (grid[0] != 0) //north to 0 not available
                        {
                        goto B1Start;
                        } else { //north to 0 is available
                        //move north to 0
                        directions.Add("N");
                        directions.Add("0");
                        grid[0] = questSteps;
                        currentLoc = 0;
                        break;
                        }

                        case 1: //check north east to 1
                        if (grid[1] != 0) //north east to 1 not available
                        {
                        goto B1Start;
                        } else { //north east to 1 is available
                        //move north east to 1
                        directions.Add("NE");
                        directions.Add("1");
                        grid[1] = questSteps;
                        currentLoc = 1;
                        break;
                        }

                        case 2: //check east to 6
                        if (grid[6] != 0) //east to 6 not available
                        {
                        goto B1Start;
                        } else { //east to 6 is available
                        //move east to 6
                        directions.Add("E");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }

                        case 3: //check south east to 11
                        if (grid[11] != 0) //south east to 11 not available
                        {
                        goto B1Start;
                        } else { //south east to 11 is available
                        //move south east to 11
                        directions.Add("SE");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }

                        case 4: //check south to 10
                        if (grid[10] != 0) //south to 10 not available
                        {
                        goto B1Start;
                        } else { //south to 10 is available
                        //move south to 10
                        directions.Add("S");
                        directions.Add("10");
                        grid[10] = questSteps;
                        currentLoc = 10;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "B2"
                case 6: //B2 out
                if (grid[0] == 0 || grid[1] == 0 || grid[2] == 0 || grid[5] == 0 || grid[7] == 0 || grid[10] == 0 || grid[11] == 0 || grid[12] == 0) //possible next (A1, A2, A3, B1, B3, C1, C2, C3), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    B2Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 0
                        if (grid[0] != 0) //north west to 0 not available
                        {
                        goto B2Start;
                        } else { //north west to 0 is available
                        //move north west to 0
                        directions.Add("NW");
                        directions.Add("0");
                        grid[0] = questSteps;
                        currentLoc = 0;
                        break;
                        }

                        case 1: //check north to 1
                        if (grid[1] != 0) //north to 1 not available
                        {
                        goto B2Start;
                        } else { //north to 1 is available
                        //move north to 1
                        directions.Add("N");
                        directions.Add("1");
                        grid[1] = questSteps;
                        currentLoc = 1;
                        break;
                        }

                        case 2: //check north east to 2
                        if (grid[2] != 0) //north east to 2 not available
                        {
                        goto B2Start;
                        } else { //north east to 2 is available
                        //move north east to 2
                        directions.Add("NE");
                        directions.Add("2");
                        grid[2] = questSteps;
                        currentLoc = 2;
                        break;
                        }

                        case 3: //check east to 7
                        if (grid[7] != 0) //south east to 7 not available
                        {
                        goto B2Start;
                        } else { //south east to 7 is available
                        //move south east to 7
                        directions.Add("E");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }

                        case 4: //check south east to 12
                        if (grid[12] != 0) //south east to 12 not available
                        {
                        goto B2Start;
                        } else { //south east to 12 is available
                        //move south east to 12
                        directions.Add("SE");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }

                        case 5: //check south to 11
                        if (grid[11] != 0) //south to 11 not available
                        {
                        goto B2Start;
                        } else { //south to 11 is available
                        //move south to 11
                        directions.Add("S");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }

                        case 6: //check south west to 10
                        if (grid[10] != 0) //south west to 10 not available
                        {
                        goto B2Start;
                        } else { //south west to 10 is available
                        //move south west to 10
                        directions.Add("SW");
                        directions.Add("10");
                        grid[10] = questSteps;
                        currentLoc = 10;
                        break;
                        }

                        case 7: //check west to 5
                        if (grid[5] != 0) //west to 5 not available
                        {
                        goto B2Start;
                        } else { //west to 5 is available
                        //move west to 5
                        directions.Add("W");
                        directions.Add("5");
                        grid[5] = questSteps;
                        currentLoc = 5;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "B3"
                case 7: //B3 out
                if (grid[1] == 0 || grid[2] == 0 || grid[3] == 0 || grid[6] == 0 || grid[8] == 0 || grid[11] == 0 || grid[12] == 0 || grid[13] == 0) //possible next (A2, A3, A4, B2, B4, C2, C3, C4), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    B3Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 1
                        if (grid[1] != 0) //north west to 1 not available
                        {
                        goto B3Start;
                        } else { //north west to 1 is available
                        //move north west to 1
                        directions.Add("NW");
                        directions.Add("1");
                        grid[1] = questSteps;
                        currentLoc = 1;
                        break;
                        }

                        case 1: //check north to 2
                        if (grid[2] != 0) //north to 2 not available
                        {
                        goto B3Start;
                        } else { //north to 2 is available
                        //move north to 2
                        directions.Add("N");
                        directions.Add("2");
                        grid[2] = questSteps;
                        currentLoc = 2;
                        break;
                        }

                        case 2: //check north east to 3
                        if (grid[3] != 0) //north east to 3 not available
                        {
                        goto B3Start;
                        } else { //north east to 3 is available
                        //move north east to 3
                        directions.Add("NE");
                        directions.Add("3");
                        grid[3] = questSteps;
                        currentLoc = 3;
                        break;
                        }

                        case 3: //check east to 8
                        if (grid[8] != 0) //south east to 8 not available
                        {
                        goto B3Start;
                        } else { //south east to 8 is available
                        //move south east to 8
                        directions.Add("E");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }

                        case 4: //check south east to 13
                        if (grid[13] != 0) //south east to 13 not available
                        {
                        goto B3Start;
                        } else { //south east to 13 is available
                        //move south east to 13
                        directions.Add("SE");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 5: //check south to 12
                        if (grid[12] != 0) //south to 12 not available
                        {
                        goto B3Start;
                        } else { //south to 12 is available
                        //move south to 12
                        directions.Add("S");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }

                        case 6: //check south west to 11
                        if (grid[11] != 0) //south west to 11 not available
                        {
                        goto B3Start;
                        } else { //south west to 11 is available
                        //move south west to 11
                        directions.Add("SW");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }

                        case 7: //check west to 6
                        if (grid[6] != 0) //west to 6 not available
                        {
                        goto B3Start;
                        } else { //west to 6 is available
                        //move west to 6
                        directions.Add("W");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "B4"
                case 8: //B4 out
                if (grid[2] == 0 || grid[3] == 0 || grid[4] == 0 || grid[7] == 0 || grid[9] == 0 || grid[12] == 0 || grid[13] == 0 || grid[14] == 0) //possible next (A3, A4, A5, B3, B5, C3, C4, C5), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    B4Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 2
                        if (grid[2] != 0) //north west to 2 not available
                        {
                        goto B4Start;
                        } else { //north west to 2 is available
                        //move north west to 2
                        directions.Add("NW");
                        directions.Add("2");
                        grid[2] = questSteps;
                        currentLoc = 2;
                        break;
                        }

                        case 1: //check north to 3
                        if (grid[3] != 0) //north to 3 not available
                        {
                        goto B4Start;
                        } else { //north to 3 is available
                        //move north to 3
                        directions.Add("N");
                        directions.Add("3");
                        grid[3] = questSteps;
                        currentLoc = 3;
                        break;
                        }

                        case 2: //check north east to 4
                        if (grid[4] != 0) //north east to 4 not available
                        {
                        goto B4Start;
                        } else { //north east to 4 is available
                        //move north east to 4
                        directions.Add("NE");
                        directions.Add("4");
                        grid[4] = questSteps;
                        currentLoc = 4;
                        break;
                        }

                        case 3: //check east to 9
                        if (grid[9] != 0) //south east to 9 not available
                        {
                        goto B4Start;
                        } else { //south east to 9 is available
                        //move south east to 9
                        directions.Add("E");
                        directions.Add("9");
                        grid[9] = questSteps;
                        currentLoc = 9;
                        break;
                        }

                        case 4: //check south east to 14
                        if (grid[14] != 0) //south east to 14 not available
                        {
                        goto B4Start;
                        } else { //south east to 14 is available
                        //move south east to 14
                        directions.Add("SE");
                        directions.Add("14");
                        grid[14] = questSteps;
                        currentLoc = 14;
                        break;
                        }

                        case 5: //check south to 13
                        if (grid[13] != 0) //south to 13 not available
                        {
                        goto B4Start;
                        } else { //south to 13 is available
                        //move south to 13
                        directions.Add("S");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 6: //check south west to 12
                        if (grid[12] != 0) //south west to 12 not available
                        {
                        goto B4Start;
                        } else { //south west to 12 is available
                        //move south west to 12
                        directions.Add("SW");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }

                        case 7: //check west to 7
                        if (grid[7] != 0) //west to 7 not available
                        {
                        goto B4Start;
                        } else { //west to 7 is available
                        //move west to 7
                        directions.Add("W");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "B5"
                case 9: //B5 out
                if (grid[3] == 0 || grid[4] == 0 || grid[8] == 0 || grid[13] == 0 || grid[14] == 0) //possible next (A4, A3, B4, C4, C5), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    B5Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 3
                        if (grid[3] != 0) //north west to 3 not available
                        {
                        goto B5Start;
                        } else { //north west to 3 is available
                        //move north west to 3
                        directions.Add("NW");
                        directions.Add("3");
                        grid[3] = questSteps;
                        currentLoc = 3;
                        break;
                        }

                        case 1: //check north to 4
                        if (grid[4] != 0) //north to 4 not available
                        {
                        goto B5Start;
                        } else { //north to 4 is available
                        //move north to 4
                        directions.Add("N");
                        directions.Add("4");
                        grid[4] = questSteps;
                        currentLoc = 4;
                        break;
                        }

                        case 2: //check south to 14
                        if (grid[14] != 0) //south to 14 not available
                        {
                        goto B5Start;
                        } else { //south to 14 is available
                        //move south to 14
                        directions.Add("S");
                        directions.Add("14");
                        grid[14] = questSteps;
                        currentLoc = 14;
                        break;
                        }

                        case 3: //check south west to 13
                        if (grid[13] != 0) //south west to 13 not available
                        {
                        goto B5Start;
                        } else { //south west to 13 is available
                        //move south west to 13
                        directions.Add("SW");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 4: //check west to 8
                        if (grid[8] != 0) //west to 8 not available
                        {
                        goto B5Start;
                        } else { //west to 8 is available
                        //move west to 8
                        directions.Add("W");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion
                    
                //Row C
                #region "C1"
                case 10: //C1 out
                if (grid[5] == 0 || grid[6] == 0 || grid[11] == 0 || grid[15] == 0 || grid[16] == 0 || grid[14] == 0) //possible next (B1, B2, C2, D2, D1, C5[Cave]), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    C1Start:
                    int rand = Random.Range(0,6); //6 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north to 5
                        if (grid[5] != 0) //north to 5 not available
                        {
                        goto C1Start;
                        } else { //north to 5 is available
                        //move north to 5
                        directions.Add("N");
                        directions.Add("5");
                        grid[5] = questSteps;
                        currentLoc = 5;
                        break;
                        }

                        case 1: //check north east to 6
                        if (grid[6] != 0) //north east to 6 not available
                        {
                        goto C1Start;
                        } else { //north east to 6 is available
                        //move north east to 6
                        directions.Add("NE");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }

                        case 2: //check east to 11
                        if (grid[11] != 0) //east to 11 not available
                        {
                        goto C1Start;
                        } else { //east to 11 is available
                        //move east to 11
                        directions.Add("E");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }

                        case 3: //check south east to 16
                        if (grid[16] != 0) //south east to 16 not available
                        {
                        goto C1Start;
                        } else { //south east to 16 is available
                        //move south east to 16
                        directions.Add("SE");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }

                        case 4: //check south to 15
                        if (grid[15] != 0) //south to 15 not available
                        {
                        goto C1Start;
                        } else { //south to 15 is available
                        //move south to 15
                        directions.Add("S");
                        directions.Add("15");
                        grid[15] = questSteps;
                        currentLoc = 15;
                        break;
                        }
                        
                        case 5: //check west to 14 [cave]
                        if (grid[14] != 0) //west to 14 not available
                        {
                        goto C1Start;
                        } else { //west to 14 is available
                        //move west to 14
                        directions.Add("W");
                        directions.Add("14");
                        grid[14] = questSteps;
                        currentLoc = 14;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "C2"
                case 11: //C2 out
                if (grid[5] == 0 || grid[6] == 0 || grid[7] == 0 || grid[10] == 0 || grid[12] == 0 || grid[15] == 0 || grid[16] == 0 || grid[17] == 0) //possible next (B1, B2, B3, C1, C3, D1, D2, D3), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    C2Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 5
                        if (grid[5] != 0) //north west to 5 not available
                        {
                        goto C2Start;
                        } else { //north west to 5 is available
                        //move north west to 5
                        directions.Add("NW");
                        directions.Add("5");
                        grid[5] = questSteps;
                        currentLoc = 5;
                        break;
                        }

                        case 1: //check north to 6
                        if (grid[6] != 0) //north to 6 not available
                        {
                        goto C2Start;
                        } else { //north to 6 is available
                        //move north to 6
                        directions.Add("N");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }

                        case 2: //check north east to 7
                        if (grid[7] != 0) //north east to 7 not available
                        {
                        goto C2Start;
                        } else { //north east to 7 is available
                        //move north east to 7
                        directions.Add("NE");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }

                        case 3: //check east to 12
                        if (grid[12] != 0) //south east to 12 not available
                        {
                        goto C2Start;
                        } else { //south east to 12 is available
                        //move south east to 12
                        directions.Add("E");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }

                        case 4: //check south east to 17
                        if (grid[17] != 0) //south east to 17 not available
                        {
                        goto C2Start;
                        } else { //south east to 17 is available
                        //move south east to 17
                        directions.Add("SE");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }

                        case 5: //check south to 16
                        if (grid[16] != 0) //south to 16 not available
                        {
                        goto C2Start;
                        } else { //south to 16 is available
                        //move south to 16
                        directions.Add("S");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }

                        case 6: //check south west to 15
                        if (grid[15] != 0) //south west to 15 not available
                        {
                        goto C2Start;
                        } else { //south west to 15 is available
                        //move south west to 15
                        directions.Add("SW");
                        directions.Add("15");
                        grid[15] = questSteps;
                        currentLoc = 15;
                        break;
                        }

                        case 7: //check west to 10
                        if (grid[10] != 0) //west to 10 not available
                        {
                        goto C2Start;
                        } else { //west to 10 is available
                        //move west to 10
                        directions.Add("W");
                        directions.Add("10");
                        grid[10] = questSteps;
                        currentLoc = 10;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "C3"
                case 12: //C3 out
                if (grid[6] == 0 || grid[7] == 0 || grid[8] == 0 || grid[11] == 0 || grid[13] == 0 || grid[16] == 0 || grid[17] == 0 || grid[18] == 0) //possible next (B2, B3, B4, C2, C4, D2, D3, D4), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    C3Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 6
                        if (grid[6] != 0) //north west to 6 not available
                        {
                        goto C3Start;
                        } else { //north west to 6 is available
                        //move north west to 6
                        directions.Add("NW");
                        directions.Add("6");
                        grid[6] = questSteps;
                        currentLoc = 6;
                        break;
                        }

                        case 1: //check north to 7
                        if (grid[7] != 0) //north to 7 not available
                        {
                        goto C3Start;
                        } else { //north to 7 is available
                        //move north to 7
                        directions.Add("N");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }

                        case 2: //check north east to 8
                        if (grid[8] != 0) //north east to 8 not available
                        {
                        goto C3Start;
                        } else { //north east to 8 is available
                        //move north east to 8
                        directions.Add("NE");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }

                        case 3: //check east to 13
                        if (grid[13] != 0) //south east to 13 not available
                        {
                        goto C3Start;
                        } else { //south east to 13 is available
                        //move south east to 13
                        directions.Add("E");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 4: //check south east to 18
                        if (grid[18] != 0) //south east to 18 not available
                        {
                        goto C3Start;
                        } else { //south east to 18 is available
                        //move south east to 18
                        directions.Add("SE");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }

                        case 5: //check south to 17
                        if (grid[17] != 0) //south to 17 not available
                        {
                        goto C3Start;
                        } else { //south to 17 is available
                        //move south to 17
                        directions.Add("S");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }

                        case 6: //check south west to 16
                        if (grid[16] != 0) //south west to 16 not available
                        {
                        goto C3Start;
                        } else { //south west to 16 is available
                        //move south west to 16
                        directions.Add("SW");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }

                        case 7: //check west to 11
                        if (grid[11] != 0) //west to 11 not available
                        {
                        goto C3Start;
                        } else { //west to 11 is available
                        //move west to 11
                        directions.Add("W");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "C4"
                case 13: //C4 out
                if (grid[7] == 0 || grid[8] == 0 || grid[9] == 0 || grid[12] == 0 || grid[14] == 0 || grid[17] == 0 || grid[18] == 0 || grid[19] == 0) //possible next (B3, B4, B5, C3, C5, D3, D4, D5), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    C4Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 7
                        if (grid[7] != 0) //north west to 7 not available
                        {
                        goto C4Start;
                        } else { //north west to 7 is available
                        //move north west to 7
                        directions.Add("NW");
                        directions.Add("7");
                        grid[7] = questSteps;
                        currentLoc = 7;
                        break;
                        }

                        case 1: //check north to 8
                        if (grid[8] != 0) //north to 8 not available
                        {
                        goto C4Start;
                        } else { //north to 8 is available
                        //move north to 8
                        directions.Add("N");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }

                        case 2: //check north east to 9
                        if (grid[9] != 0) //north east to 9 not available
                        {
                        goto C4Start;
                        } else { //north east to 9 is available
                        //move north east to 9
                        directions.Add("NE");
                        directions.Add("9");
                        grid[9] = questSteps;
                        currentLoc = 9;
                        break;
                        }

                        case 3: //check east to 14
                        if (grid[14] != 0) //south east to 14 not available
                        {
                        goto C4Start;
                        } else { //south east to 14 is available
                        //move south east to 14
                        directions.Add("E");
                        directions.Add("14");
                        grid[14] = questSteps;
                        currentLoc = 14;
                        break;
                        }

                        case 4: //check south east to 19
                        if (grid[19] != 0) //south east to 19 not available
                        {
                        goto C4Start;
                        } else { //south east to 19 is available
                        //move south east to 19
                        directions.Add("SE");
                        directions.Add("19");
                        grid[19] = questSteps;
                        currentLoc = 19;
                        break;
                        }

                        case 5: //check south to 18
                        if (grid[18] != 0) //south to 18 not available
                        {
                        goto C4Start;
                        } else { //south to 18 is available
                        //move south to 18
                        directions.Add("S");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }

                        case 6: //check south west to 17
                        if (grid[17] != 0) //south west to 17 not available
                        {
                        goto C4Start;
                        } else { //south west to 17 is available
                        //move south west to 17
                        directions.Add("SW");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }

                        case 7: //check west to 12
                        if (grid[12] != 0) //west to 12 not available
                        {
                        goto C4Start;
                        } else { //west to 12 is available
                        //move west to 12
                        directions.Add("W");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "C5"
                case 14: //C5 out
                if (grid[8] == 0 || grid[9] == 0 || grid[13] == 0 || grid[18] == 0 || grid[19] == 0 || grid[10] == 0) //possible next (B4, B3, C4, D4, D5, C1[Cave]), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    C5Start:
                    int rand = Random.Range(0,6); //6 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 8
                        if (grid[8] != 0) //north west to 8 not available
                        {
                        goto C5Start;
                        } else { //north west to 8 is available
                        //move north west to 8
                        directions.Add("NW");
                        directions.Add("8");
                        grid[8] = questSteps;
                        currentLoc = 8;
                        break;
                        }

                        case 1: //check north to 9
                        if (grid[9] != 0) //north to 9 not available
                        {
                        goto C5Start;
                        } else { //north to 9 is available
                        //move north to 9
                        directions.Add("N");
                        directions.Add("9");
                        grid[9] = questSteps;
                        currentLoc = 9;
                        break;
                        }

                        case 2: //check south to 19
                        if (grid[19] != 0) //south to 19 not available
                        {
                        goto C5Start;
                        } else { //south to 19 is available
                        //move south to 19
                        directions.Add("S");
                        directions.Add("19");
                        grid[19] = questSteps;
                        currentLoc = 19;
                        break;
                        }

                        case 3: //check south west to 18
                        if (grid[18] != 0) //south west to 18 not available
                        {
                        goto C5Start;
                        } else { //south west to 18 is available
                        //move south west to 18
                        directions.Add("SW");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }

                        case 4: //check west to 13
                        if (grid[13] != 0) //west to 13 not available
                        {
                        goto C5Start;
                        } else { //west to 13 is available
                        //move west to 13
                        directions.Add("W");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 5: //check east to 10
                        if (grid[10] != 0) //east to 10 not available
                        {
                        goto C5Start;
                        } else { //east to 10 is available
                        //move east to 10
                        directions.Add("E");
                        directions.Add("10");
                        grid[10] = questSteps;
                        currentLoc = 10;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion
                    
                //Row D
                #region "D1"
                case 15: //D1 out
                if (grid[10] == 0 || grid[11] == 0 || grid[16] == 0 || grid[21] == 0 || grid[20] == 0) //possible next (C1, C2, D2, E2, E1), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    D1Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north to 10
                        if (grid[10] != 0) //north to 10 not available
                        {
                        goto D1Start;
                        } else { //north to 10 is available
                        //move north to 10
                        directions.Add("N");
                        directions.Add("10");
                        grid[10] = questSteps;
                        currentLoc = 10;
                        break;
                        }

                        case 1: //check north east to 11
                        if (grid[11] != 0) //north east to 11 not available
                        {
                        goto D1Start;
                        } else { //north east to 11 is available
                        //move north east to 11
                        directions.Add("NE");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }

                        case 2: //check east to 16
                        if (grid[16] != 0) //east to 16 not available
                        {
                        goto D1Start;
                        } else { //east to 16 is available
                        //move east to 16
                        directions.Add("E");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }

                        case 3: //check south east to 21
                        if (grid[21] != 0) //south east to 21 not available
                        {
                        goto D1Start;
                        } else { //south east to 21 is available
                        //move south east to 21
                        directions.Add("SE");
                        directions.Add("21");
                        grid[21] = questSteps;
                        currentLoc = 21;
                        break;
                        }

                        case 4: //check south to 20
                        if (grid[20] != 0) //south to 20 not available
                        {
                        goto D1Start;
                        } else { //south to 20 is available
                        //move south to 20
                        directions.Add("S");
                        directions.Add("20");
                        grid[20] = questSteps;
                        currentLoc = 20;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "D2"
                case 16: //D2 out
                if (grid[10] == 0 || grid[11] == 0 || grid[12] == 0 || grid[15] == 0 || grid[17] == 0 || grid[20] == 0 || grid[21] == 0 || grid[22] == 0) //possible next (C1, C2, C3, D1, D3, E1, E2, E3), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    D2Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 0
                        if (grid[10] != 0) //north west to 10 not available
                        {
                        goto D2Start;
                        } else { //north west to 10 is available
                        //move north west to 10
                        directions.Add("NW");
                        directions.Add("10");
                        grid[10] = questSteps;
                        currentLoc = 10;
                        break;
                        }

                        case 1: //check north to 11
                        if (grid[11] != 0) //north to 11 not available
                        {
                        goto D2Start;
                        } else { //north to 11 is available
                        //move north to 11
                        directions.Add("N");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }

                        case 2: //check north east to 12
                        if (grid[12] != 0) //north east to 12 not available
                        {
                        goto D2Start;
                        } else { //north east to 12 is available
                        //move north east to 12
                        directions.Add("NE");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }

                        case 3: //check east to 17
                        if (grid[17] != 0) //south east to 17 not available
                        {
                        goto D2Start;
                        } else { //south east to 17 is available
                        //move south east to 17
                        directions.Add("E");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }

                        case 4: //check south east to 22
                        if (grid[22] != 0) //south east to 22 not available
                        {
                        goto D2Start;
                        } else { //south east to 22 is available
                        //move south east to 22
                        directions.Add("SE");
                        directions.Add("22");
                        grid[22] = questSteps;
                        currentLoc = 22;
                        break;
                        }

                        case 5: //check south to 21
                        if (grid[21] != 0) //south to 21 not available
                        {
                        goto D2Start;
                        } else { //south to 21 is available
                        //move south to 21
                        directions.Add("S");
                        directions.Add("21");
                        grid[21] = questSteps;
                        currentLoc = 21;
                        break;
                        }

                        case 6: //check south west to 20
                        if (grid[20] != 0) //south west to 20 not available
                        {
                        goto D2Start;
                        } else { //south west to 20 is available
                        //move south west to 20
                        directions.Add("SW");
                        directions.Add("20");
                        grid[20] = questSteps;
                        currentLoc = 20;
                        break;
                        }

                        case 7: //check west to 15
                        if (grid[15] != 0) //west to 15 not available
                        {
                        goto D2Start;
                        } else { //west to 15 is available
                        //move west to 15
                        directions.Add("W");
                        directions.Add("15");
                        grid[15] = questSteps;
                        currentLoc = 15;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "D3"
                case 17: //D3 out
                if (grid[11] == 0 || grid[12] == 0 || grid[13] == 0 || grid[16] == 0 || grid[18] == 0 || grid[21] == 0 || grid[22] == 0 || grid[23] == 0) //possible next (C2, C3, C4, D2, D4, E2, E3, E4), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    D3Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 11
                        if (grid[11] != 0) //north west to 11 not available
                        {
                        goto D3Start;
                        } else { //north west to 11 is available
                        //move north west to 11
                        directions.Add("NW");
                        directions.Add("11");
                        grid[11] = questSteps;
                        currentLoc = 11;
                        break;
                        }

                        case 1: //check north to 12
                        if (grid[12] != 0) //north to 12 not available
                        {
                        goto D3Start;
                        } else { //north to 12 is available
                        //move north to 12
                        directions.Add("N");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }

                        case 2: //check north east to 13
                        if (grid[13] != 0) //north east to 13 not available
                        {
                        goto D3Start;
                        } else { //north east to 13 is available
                        //move north east to 13
                        directions.Add("NE");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 3: //check east to 18
                        if (grid[18] != 0) //south east to 18 not available
                        {
                        goto D3Start;
                        } else { //south east to 18 is available
                        //move south east to 18
                        directions.Add("E");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }

                        case 4: //check south east to 23
                        if (grid[23] != 0) //south east to 23 not available
                        {
                        goto D3Start;
                        } else { //south east to 23 is available
                        //move south east to 23
                        directions.Add("SE");
                        directions.Add("23");
                        grid[23] = questSteps;
                        currentLoc = 23;
                        break;
                        }

                        case 5: //check south to 22
                        if (grid[22] != 0) //south to 22 not available
                        {
                        goto D3Start;
                        } else { //south to 22 is available
                        //move south to 22
                        directions.Add("S");
                        directions.Add("22");
                        grid[22] = questSteps;
                        currentLoc = 22;
                        break;
                        }

                        case 6: //check south west to 21
                        if (grid[21] != 0) //south west to 21 not available
                        {
                        goto D3Start;
                        } else { //south west to 21 is available
                        //move south west to 21
                        directions.Add("SW");
                        directions.Add("21");
                        grid[21] = questSteps;
                        currentLoc = 21;
                        break;
                        }

                        case 7: //check west to 16
                        if (grid[16] != 0) //west to 16 not available
                        {
                        goto D3Start;
                        } else { //west to 16 is available
                        //move west to 16
                        directions.Add("W");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "D4"
                case 18: //D4 out
                if (grid[12] == 0 || grid[13] == 0 || grid[14] == 0 || grid[17] == 0 || grid[19] == 0 || grid[22] == 0 || grid[23] == 0 || grid[24] == 0) //possible next (C3, C4, C5, D3, D5, E3, E4, E5), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    D4Start:
                    int rand = Random.Range(0,8); //8 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 12
                        if (grid[12] != 0) //north west to 12 not available
                        {
                        goto D4Start;
                        } else { //north west to 12 is available
                        //move north west to 12
                        directions.Add("NW");
                        directions.Add("12");
                        grid[12] = questSteps;
                        currentLoc = 12;
                        break;
                        }

                        case 1: //check north to 13
                        if (grid[13] != 0) //north to 13 not available
                        {
                        goto D4Start;
                        } else { //north to 13 is available
                        //move north to 13
                        directions.Add("N");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 2: //check north east to 14
                        if (grid[14] != 0) //north east to 14 not available
                        {
                        goto D4Start;
                        } else { //north east to 14 is available
                        //move north east to 14
                        directions.Add("NE");
                        directions.Add("14");
                        grid[14] = questSteps;
                        currentLoc = 14;
                        break;
                        }

                        case 3: //check east to 19
                        if (grid[19] != 0) //south east to 19 not available
                        {
                        goto D4Start;
                        } else { //south east to 19 is available
                        //move south east to 19
                        directions.Add("E");
                        directions.Add("19");
                        grid[19] = questSteps;
                        currentLoc = 19;
                        break;
                        }

                        case 4: //check south east to 24
                        if (grid[24] != 0) //south east to 24 not available
                        {
                        goto D4Start;
                        } else { //south east to 24 is available
                        //move south east to 24
                        directions.Add("SE");
                        directions.Add("24");
                        grid[24] = questSteps;
                        currentLoc = 24;
                        break;
                        }

                        case 5: //check south to 23
                        if (grid[23] != 0) //south to 23 not available
                        {
                        goto D4Start;
                        } else { //south to 23 is available
                        //move south to 23
                        directions.Add("S");
                        directions.Add("23");
                        grid[23] = questSteps;
                        currentLoc = 23;
                        break;
                        }

                        case 6: //check south west to 22
                        if (grid[22] != 0) //south west to 22 not available
                        {
                        goto D4Start;
                        } else { //south west to 22 is available
                        //move south west to 22
                        directions.Add("SW");
                        directions.Add("22");
                        grid[22] = questSteps;
                        currentLoc = 22;
                        break;
                        }

                        case 7: //check west to 17
                        if (grid[17] != 0) //west to 17 not available
                        {
                        goto D4Start;
                        } else { //west to 17 is available
                        //move west to 17
                        directions.Add("W");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "D5"
                case 19: //D5 out
                if (grid[13] == 0 || grid[14] == 0 || grid[18] == 0 || grid[23] == 0 || grid[24] == 0) //possible next (C4, C3, D4, E4, E5), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    D5Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 13
                        if (grid[13] != 0) //north west to 13 not available
                        {
                        goto D5Start;
                        } else { //north west to 13 is available
                        //move north west to 13
                        directions.Add("NW");
                        directions.Add("13");
                        grid[13] = questSteps;
                        currentLoc = 13;
                        break;
                        }

                        case 1: //check north to 14
                        if (grid[14] != 0) //north to 14 not available
                        {
                        goto D5Start;
                        } else { //north to 14 is available
                        //move north to 14
                        directions.Add("N");
                        directions.Add("14");
                        grid[14] = questSteps;
                        currentLoc = 14;
                        break;
                        }

                        case 2: //check south to 24
                        if (grid[24] != 0) //south to 24 not available
                        {
                        goto D5Start;
                        } else { //south to 24 is available
                        //move south to 24
                        directions.Add("S");
                        directions.Add("24");
                        grid[24] = questSteps;
                        currentLoc = 24;
                        break;
                        }

                        case 3: //check south west to 23
                        if (grid[23] != 0) //south west to 23 not available
                        {
                        goto D5Start;
                        } else { //south west to 23 is available
                        //move south west to 23
                        directions.Add("SW");
                        directions.Add("23");
                        grid[23] = questSteps;
                        currentLoc = 23;
                        break;
                        }

                        case 4: //check west to 18
                        if (grid[18] != 0) //west to 18 not available
                        {
                        goto D5Start;
                        } else { //west to 18 is available
                        //move west to 18
                        directions.Add("W");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                //Row E
                #region "E1"
                case 20: //E1 out
                if (grid[15] == 0 || grid[16] == 0 || grid[21] == 0) //possible next (D1, D2, E2), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    E1Start:
                    int rand = Random.Range(0,3); //3 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north to 15
                        if (grid[15] != 0) //north to 15 not available
                        {
                        goto E1Start;
                        } else { //north to 15 is available
                        //move north to 15
                        directions.Add("N");
                        directions.Add("15");
                        grid[15] = questSteps;
                        currentLoc = 15;
                        break;
                        }

                        case 1: //check north east to 16
                        if (grid[16] != 0) //north east to 16 not available
                        {
                        goto E1Start;
                        } else { //north east to 16 is available
                        //move north east to 16
                        directions.Add("NE");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }

                        case 2: //check east to 21
                        if (grid[21] != 0) //east to 21 not available
                        {
                        goto E1Start;
                        } else { //east to 21 is available
                        //move east to 21
                        directions.Add("E");
                        directions.Add("21");
                        grid[21] = questSteps;
                        currentLoc = 21;
                        break;
                        }

                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "E2"
                case 21: //E2 out
                if (grid[15] == 0 || grid[16] == 0 || grid[17] == 0 || grid[20] == 0 || grid[22] == 0) //possible next (D1, D2, D3, E1, E3), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    E2Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 15
                        if (grid[15] != 0) //north west to 15 not available
                        {
                        goto E2Start;
                        } else { //north west to 15 is available
                        //move north west to 15
                        directions.Add("NW");
                        directions.Add("15");
                        grid[15] = questSteps;
                        currentLoc = 15;
                        break;
                        }

                        case 1: //check north to 16
                        if (grid[16] != 0) //north to 16 not available
                        {
                        goto E2Start;
                        } else { //north to 16 is available
                        //move north to 16
                        directions.Add("N");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }

                        case 2: //check north east to 17
                        if (grid[17] != 0) //north east to 17 not available
                        {
                        goto E2Start;
                        } else { //north east to 17 is available
                        //move north east to 17
                        directions.Add("NE");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }

                        case 3: //check east to 22
                        if (grid[22] != 0) //east to 22 not available
                        {
                        goto E2Start;
                        } else { //east to 22 is available
                        //move east to 22
                        directions.Add("E");
                        directions.Add("22");
                        grid[22] = questSteps;
                        currentLoc = 22;
                        break;
                        }

                        case 4: //check west to 20
                        if (grid[20] != 0) //west to 20 not available
                        {
                        goto E2Start;
                        } else { //west to 20 is available
                        //move west to 20
                        directions.Add("W");
                        directions.Add("20");
                        grid[20] = questSteps;
                        currentLoc = 20;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "E3"
                case 22: //E3 out
                if (grid[16] == 0 || grid[17] == 0 || grid[18] == 0 || grid[21] == 0 || grid[23] == 0) //possible next (D2, D3, D4, E2, E4), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    E3Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 16
                        if (grid[16] != 0) //north west to 16 not available
                        {
                        goto E3Start;
                        } else { //north west to 16 is available
                        //move north west to 16
                        directions.Add("NW");
                        directions.Add("16");
                        grid[16] = questSteps;
                        currentLoc = 16;
                        break;
                        }

                        case 1: //check north to 17
                        if (grid[17] != 0) //north to 17 not available
                        {
                        goto E3Start;
                        } else { //north to 17 is available
                        //move north to 17
                        directions.Add("N");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }

                        case 2: //check north east to 18
                        if (grid[18] != 0) //north east to 18 not available
                        {
                        goto E3Start;
                        } else { //north east to 18 is available
                        //move north east to 18
                        directions.Add("NE");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }

                        case 3: //check east to 23
                        if (grid[23] != 0) //south east to 23 not available
                        {
                        goto E3Start;
                        } else { //south east to 23 is available
                        //move south east to 23
                        directions.Add("E");
                        directions.Add("23");
                        grid[23] = questSteps;
                        currentLoc = 23;
                        break;
                        }

                        case 4: //check west to 21
                        if (grid[21] != 0) //west to 21 not available
                        {
                        goto E3Start;
                        } else { //west to 21 is available
                        //move west to 21
                        directions.Add("W");
                        directions.Add("21");
                        grid[21] = questSteps;
                        currentLoc = 21;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "E4"
                case 23: //E4 out
                if (grid[17] == 0 || grid[18] == 0 || grid[19] == 0 || grid[22] == 0 || grid[24] == 0) //possible next (D3, D4, D5, E3, E5), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    E4Start:
                    int rand = Random.Range(0,5); //5 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 17
                        if (grid[17] != 0) //north west to 17 not available
                        {
                        goto E4Start;
                        } else { //north west to 17 is available
                        //move north west to 17
                        directions.Add("NW");
                        directions.Add("17");
                        grid[17] = questSteps;
                        currentLoc = 17;
                        break;
                        }

                        case 1: //check north to 18
                        if (grid[18] != 0) //north to 18 not available
                        {
                        goto E4Start;
                        } else { //north to 18 is available
                        //move north to 18
                        directions.Add("N");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }

                        case 2: //check north east to 19
                        if (grid[19] != 0) //north east to 19 not available
                        {
                        goto E4Start;
                        } else { //north east to 19 is available
                        //move north east to 19
                        directions.Add("NE");
                        directions.Add("19");
                        grid[19] = questSteps;
                        currentLoc = 19;
                        break;
                        }

                        case 3: //check east to 24
                        if (grid[24] != 0) //south east to 24 not available
                        {
                        goto E4Start;
                        } else { //south east to 24 is available
                        //move south east to 24
                        directions.Add("E");
                        directions.Add("24");
                        grid[24] = questSteps;
                        currentLoc = 24;
                        break;
                        }

                        case 4: //check west to 22
                        if (grid[22] != 0) //west to 22 not available
                        {
                        goto E4Start;
                        } else { //west to 22 is available
                        //move west to 22
                        directions.Add("W");
                        directions.Add("22");
                        grid[22] = questSteps;
                        currentLoc = 22;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                #region "E5"
                case 24: //E5 out
                if (grid[18] == 0 || grid[19] == 0 || grid[23] == 0) //possible next (B4, B3, C4), one possible next path
                {
                    questSteps -= 1; //remove one from questSteps
                    E5Start:
                    int rand = Random.Range(0,3); //3 possible next tiles
                    switch (rand)
                    {
                        case 0: //check north west to 18
                        if (grid[18] != 0) //north west to 18 not available
                        {
                        goto E5Start;
                        } else { //north west to 18 is available
                        //move north west to 18
                        directions.Add("NW");
                        directions.Add("18");
                        grid[18] = questSteps;
                        currentLoc = 18;
                        break;
                        }

                        case 1: //check north to 19
                        if (grid[19] != 0) //north to 19 not available
                        {
                        goto E5Start;
                        } else { //north to 19 is available
                        //move north to 19
                        directions.Add("N");
                        directions.Add("19");
                        grid[19] = questSteps;
                        currentLoc = 19;
                        break;
                        }

                        case 2: //check west to 23
                        if (grid[23] != 0) //west to 23 not available
                        {
                        goto E5Start;
                        } else { //west to 23 is available
                        //move west to 23
                        directions.Add("W");
                        directions.Add("23");
                        grid[23] = questSteps;
                        currentLoc = 23;
                        break;
                        }
                    }
                    return true;
                } else{
                    return false;
                }
                #endregion

                // =====================
                default:
                return false;

            } 

    }

#endregion
#region "Instantiate Tiles"
    void InstantiateTiles()
    {
        //Roll random tiles, set to generic for testing
        
        Instantiate(genFloor, waypoints[int.Parse(directions[0])].transform.position, Quaternion.identity);
        Instantiate(genFloor, waypoints[int.Parse(directions[2])].transform.position, Quaternion.identity);
        Instantiate(genFloor, waypoints[int.Parse(directions[4])].transform.position, Quaternion.identity);
        Instantiate(genFloor, waypoints[int.Parse(directions[6])].transform.position, Quaternion.identity);
        Instantiate(genFloor, waypoints[int.Parse(directions[8])].transform.position, Quaternion.identity);
        Instantiate(genFloor, waypoints[int.Parse(directions[10])].transform.position, Quaternion.identity);
        
        //Last tile set to boss
        Instantiate(genFloor, waypoints[int.Parse(directions[12])].transform.position, Quaternion.identity);

        for (int i = 0; i < tiles; i++)
        {
            if (i != int.Parse(directions[0]) && i != int.Parse(directions[2]) && i != int.Parse(directions[4]) && i != int.Parse(directions[6]) && i != int.Parse(directions[8]) && i != int.Parse(directions[10]) && i != int.Parse(directions[12]))
            {
                Instantiate(emptyFloor, waypoints[i].transform.position, Quaternion.identity);
            }
        }
    }
#endregion
#region "Waypoints to Manager"
    void SendWaypointsToManger()
    {
        wpm.AddWaypoint(waypoints[int.Parse(directions[0])].transform.position);
        wpm.AddWaypoint(waypoints[int.Parse(directions[2])].transform.position);
        wpm.AddWaypoint(waypoints[int.Parse(directions[4])].transform.position);
        wpm.AddWaypoint(waypoints[int.Parse(directions[6])].transform.position);
        wpm.AddWaypoint(waypoints[int.Parse(directions[8])].transform.position);
        wpm.AddWaypoint(waypoints[int.Parse(directions[10])].transform.position);
        wpm.AddWaypoint(waypoints[int.Parse(directions[12])].transform.position);

    }

#endregion
}
