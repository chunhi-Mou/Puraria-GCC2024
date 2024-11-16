using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PathfindingAStar : MonoBehaviour
{
    public GridMapController grid;
    List<Node> openNodes = new List<Node>();//các nút được mở nhưng chưa đánh giá value
    List<Node> closeNodes = new List<Node>();

    void Start(){
        grid = GetComponent<GridMapController>();
    }

    public List<Vector2> resultPath(Vector2 startPos, Vector2 endPos, int SoilType){//lưu kết quả
        List<Vector2> pathVect = new List<Vector2>();
        Node startNode = grid.GetNode(startPos);
        Node endNode = grid.GetNode(endPos);
        
        if(startNode==null|| endNode==null){
            Debug.Log("Invalid Position");
            pathVect.Add(startPos);
            return pathVect;
        }

        openNodes = new List<Node>{startNode};
        closeNodes=new List<Node>();
        for (int y = 0; y <= grid.ySize; y++) // hàng
        {
            for (int x = 0; x <= grid.xSize; x++) //cột
            {
                Node pathNode = grid.GetNodeInt(x,y);
                pathNode.gCost=9999999;
                pathNode.prevNode=null;
            }
        }

        startNode.gCost=0;
        startNode.hCost=CalcDist(startNode.pos, endNode.pos);
        startNode.FCost= startNode.gCost+ startNode.hCost;

        while(openNodes.Count>0){
            Node currNode = GetLowestF();
            if(currNode == endNode){
                return CalcPath(currNode);
            }
            openNodes.Remove(currNode);
            closeNodes.Add(currNode);

            foreach(Node neighbour in GetNeighbours(currNode)){
                if(closeNodes.Contains(neighbour)) continue;
                if(neighbour.soilType>SoilType) {
                    closeNodes.Add(neighbour);
                    continue;
                    }
                else {
                    float newGCost= currNode.gCost+1;
                    if(newGCost<neighbour.gCost){
                        neighbour.prevNode= currNode;
                        neighbour.gCost=newGCost;
                        neighbour.hCost=CalcDist(neighbour.pos,endNode.pos);
                        neighbour.FCost=neighbour.gCost+neighbour.hCost;
                        if(!openNodes.Contains(neighbour)) openNodes.Add(neighbour);
                    }
                }
            }
        }

        //không tìm thấy đường
        Debug.Log("No Path Found");
        pathVect.Add(startPos);
        return pathVect;
    
    }

    private float CalcDist(Vector2 a, Vector2 b){//tính khoảng cách ->hCost
        float xDist = Math.Abs(a.x-b.x);
        float yDist = Math.Abs(a.y-b.y);
        return xDist + yDist;
    }

    private Node GetLowestF(){
        Node lowestF = openNodes[0];
        for(int i=0;i<openNodes.Count;i++){
            if(openNodes[i].FCost<lowestF.FCost) lowestF= openNodes[i];
        }
        return lowestF;
    }

    private List<Node> GetNeighbours( Node currNode){
        List<Node> neighboursList = new List<Node>();
        if(currNode.pos.x -1>0) neighboursList.Add(grid.GetNode(currNode.pos- new Vector2(1,0)));
        if(currNode.pos.x +1>grid.xSize) neighboursList.Add(grid.GetNode(currNode.pos+ new Vector2(1,0)));
        
        if(Mathf.RoundToInt((currNode.y +0.86f)/0.86f)<grid.ySize){
            if(currNode.pos.x -1>0) neighboursList.Add(grid.GetNode(currNode.pos + new Vector2(-1,0.86f)));
            if(currNode.pos.x +1>grid.xSize) neighboursList.Add(grid.GetNode(currNode.pos+ new Vector2(1,0.86f)));
        
        }

        if(currNode.y -0.86f>0){
            if(currNode.pos.x -1>0) neighboursList.Add(grid.GetNode(currNode.pos + new Vector2(-1,-0.86f)));
            if(currNode.pos.x +1>grid.xSize) neighboursList.Add(grid.GetNode(currNode.pos+ new Vector2(1,-0.86f)));
        
        }

        return neighboursList;
    }

    private List<Vector2> CalcPath(Node endNode){
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currNode = endNode;
        while(currNode.prevNode!= null){
            path.Add(currNode.prevNode);
            currNode=currNode.prevNode;
        }
        path.Reverse();
        List<Vector2>pathVect = new List<Vector2>();
        for(int i=0;i<path.Count;i++){
            pathVect.Add(path[i].pos);
        }
        return pathVect;
    }

    
}



