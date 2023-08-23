using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MultipleBranchSystem
{
    public class TreeNode<T>
    {
        public T Data{get;set;}

        public List<TreeNode<T>>ChildList{get;set;}

        public TreeNode(T value)
        {
            Data=value;
            ChildList=new List<TreeNode<T>>();
        }
    }
    public class Tree<T>
    {
        public TreeNode<T> Root{get;set;}//根节点
        
        //构造函数
        public Tree(T rootValue)
        {
            Root=new TreeNode<T>(rootValue);
        }
        //添加子节点
        public void AddChild(TreeNode<T>parent,T value)
        {
            TreeNode<T>childNode=new TreeNode<T>(value);
            parent.ChildList.Add(childNode);
        }
        //打印树
        public void PrintTree(TreeNode<T>node)
        {
            Debug.Log(node.Data.GetType().Name);
            foreach(var child in node.ChildList)
            {
                PrintTree(node);
            }
        }
    }
}

