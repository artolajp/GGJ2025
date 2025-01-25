using System.Collections;
using System.Collections.Generic;
using GGJ2025;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridControllerShould
{
    private GridController<GridObject> gridController;
    
    [SetUp]
    public void Setup()
    {
        gridController = new GridController<GridObject>(16, 16);
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void Add_element()
    {
        var dummy = new GridObject();
        
        gridController.TryAdd(0,0, dummy);
        
        Assert.AreEqual(gridController[0,0], dummy);
    }
    
    [Test]
    public void Remove_element()
    {
        var dummy = new GridObject();
        
        gridController.TryAdd(0,0, dummy);
        gridController.TryRemove(dummy);
        
        Assert.AreEqual(gridController[0,0], null);
    }
    
    [Test]
    public void Add_big_element()
    {
        var dummy = new GridObject(2,2);
        
        bool result = gridController.TryAdd(0,0, dummy);
        
        Assert.IsTrue(result);
    }
    
    [Test]
    public void No_add_element_bigger_than_grid()
    {
        var dummy = new GridObject(20,20);
        
        bool result = gridController.TryAdd(0,0, dummy);
        
        Assert.IsFalse(result);
    }
    
    [Test]
    public void No_add_element_on_occupied_grid()
    {
        var obj1 = new GridObject();
        var obj2 = new GridObject();
        
        bool result = gridController.TryAdd(0,0, obj1);
        
        Assert.IsTrue(result);
        
        result = gridController.TryAdd(0,0, obj2);
        
        Assert.IsFalse(result);
    }
    
    [Test]
    public void No_add_big_element_on_occupied_grid()
    {
        var obj1 = new GridObject();
        var obj2 = new GridObject(2,2);
        
        bool result = gridController.TryAdd(1,1, obj1);
        Assert.IsTrue(result);
        
        result = gridController.TryAdd(0,0, obj2);
        
        Assert.IsFalse(result);
    }
    
    [Test]
    public void Add_big_element_and_occupied_several_positions()
    {
        var obj2 = new GridObject(2,2);
        
        var result = gridController.TryAdd(0,0, obj2);
        Assert.IsTrue(result);
        Assert.IsFalse(gridController.IsEmpty(0,0));
        Assert.IsFalse(gridController.IsEmpty(1,0));
        Assert.IsFalse(gridController.IsEmpty(0,1));
        Assert.IsFalse(gridController.IsEmpty(1,1));
    }
    
    [Test]
    public void Remove_big_element_free_all_positions()
    {
        var obj2 = new GridObject(2,2);
        
        var result = gridController.TryAdd(0,0, obj2);
        Assert.IsTrue(result);
        Assert.IsFalse(gridController.IsEmpty(0,0));
        Assert.IsFalse(gridController.IsEmpty(1,0));
        Assert.IsFalse(gridController.IsEmpty(0,1));
        Assert.IsFalse(gridController.IsEmpty(1,1));
        
        result = gridController.TryRemove(obj2);
        Assert.IsTrue(result);
        Assert.IsTrue(gridController.IsEmpty(0,0));
        Assert.IsTrue(gridController.IsEmpty(1,0));
        Assert.IsTrue(gridController.IsEmpty(0,1));
        Assert.IsTrue(gridController.IsEmpty(1,1));
    }

    [Test]
    public void Get_element_on_position()
    {
        var obj2 = new GridObject(2,2);
        
        bool result = gridController.TryAdd(0,0, obj2);
        
        Assert.IsTrue(result);
        Assert.AreEqual(obj2, gridController[0,0]);
    }
    
    [Test]
    public void Check_if_can_place_element()
    {
        var obj2 = new GridObject(2,2);
        
        bool result = gridController.IsEmpty(0, 0, obj2);
        
        Assert.IsTrue(result);
    }
    
    [Test]
    public void Check_if_can_NOT_place_element()
    {
        var obj1 = new GridObject(1,1);
        bool result = gridController.TryAdd(1,1, obj1);
        Assert.IsTrue(result);
        
        var obj2 = new GridObject(2,2);
        result = gridController.IsEmpty(1, 1, obj2);
        Assert.IsFalse(result);
        result = gridController.IsEmpty(0, 0, obj2);
        Assert.IsFalse(result);
        result = gridController.IsEmpty(1, 0, obj2);
        Assert.IsFalse(result);
        result = gridController.IsEmpty(0, 1, obj2);
        Assert.IsFalse(result);
    }

    [Test]
    public void Can_NOT_place_element_out_of_grid()
    {
        var obj1 = new GridObject(1,1);
        
        bool result = gridController.TryAdd(30,30, obj1);
        Assert.IsFalse(result);
    }
    
    [Test]
    public void Can_NOT_place_element_out_of_grid_negative_position()
    {
        var obj1 = new GridObject(1,1);
        
        bool result = gridController.TryAdd(-1,0, obj1);
        Assert.IsFalse(result);
    }
    
    [Test]
    public void Can_NOT_place_big_element_out_of_grid()
    {
        var obj1 = new GridObject(2,2);
        
        bool result = gridController.TryAdd(15,15, obj1);
        Assert.IsFalse(result);
    }
    
    [Test]
    public void Get_empty_cells()
    {
        gridController = new GridController<GridObject>(3, 3);
        
        var obj1 = new GridObject(2,2);
        
        bool result = gridController.TryAdd(0,0, obj1);
        Assert.IsTrue(result);

        List<Vector2Int> emptyCells = gridController.GetEmpties();
        
        Assert.IsTrue(emptyCells.Count == 5);
        Assert.Contains(new Vector2Int(2, 0), emptyCells);
        Assert.Contains(new Vector2Int(2, 1), emptyCells);
        Assert.Contains(new Vector2Int(0, 2), emptyCells);
        Assert.Contains(new Vector2Int(1, 2), emptyCells);
        Assert.Contains(new Vector2Int(2, 2), emptyCells);
    }
    
    [Test]
    public void Get_occupied_cells()
    {
        gridController = new GridController<GridObject>(3, 3);
        
        var obj1 = new GridObject(2,2);
        
        bool result = gridController.TryAdd(0,0, obj1);
        Assert.IsTrue(result);

        List<Vector2Int> occupiedCells = gridController.GetOccupied();
        
        Assert.IsTrue(occupiedCells.Count == 4);
        Assert.Contains(new Vector2Int(0, 0), occupiedCells);
        Assert.Contains(new Vector2Int(1, 0), occupiedCells);
        Assert.Contains(new Vector2Int(0, 1), occupiedCells);
        Assert.Contains(new Vector2Int(1, 1), occupiedCells);
    }
    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GridControllerShouldWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
