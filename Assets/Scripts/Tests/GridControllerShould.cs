using System.Collections;
using GGJ2025;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridControllerShould
{
    private GridController<ExampleClass> gridController;

    private class ExampleClass
    {
        
    }
    
    [SetUp]
    public void Setup()
    {
        gridController = new GridController<ExampleClass>(16, 16);
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void Add_element()
    {
        var dummy = new ExampleClass();
        
        gridController.Add(0,0, dummy);
        
        Assert.AreEqual(gridController[0,0], dummy);
    }
    
    [Test]
    public void Remove_element()
    {
        var dummy = new ExampleClass();
        
        gridController.Add(0,0, dummy);
        gridController.Remove(0,0);
        
        Assert.AreEqual(gridController[0,0], null);
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
