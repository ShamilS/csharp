using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


[TestFixture]
public class YachtTests
{
    [SetUp] public void Setup() { }
    [TearDown] public void TearDown() { }

    //  from: https://raw.githubusercontent.com/exercism/problem-specifications/master/exercises/yacht/canonical-data.json

    [Test]
    public void Yacht()
    {
        var actual = new Yacht().CalculateScore(new int[] { 5,5,5,5,5}, "yacht");
        var expected = 50;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void NotYacht()
    {
        var actual = new Yacht().CalculateScore(new int[] { 1, 3, 3, 2, 5 }, "yacht");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Ones()
    {
        var actual = new Yacht().CalculateScore(new int[] { 1, 1, 1, 3, 5}, "ones");
        var expected = 3;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void OnesAreOutOfOrder()
    {
        var actual = new Yacht().CalculateScore(new int[] { 3, 1, 1, 5, 1 }, "ones");
        var expected = 3;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void NoOnes()
    {
        var actual = new Yacht().CalculateScore(new int[] { 4, 3, 6, 5, 5 }, "ones");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Twos()
    {
        var actual = new Yacht().CalculateScore(new int[] { 2, 3, 4, 5, 6 }, "twos");
        var expected = 2;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Fours()
    {
        var actual = new Yacht().CalculateScore(new int[] { 1, 4, 1, 4, 1  }, "fours");
        var expected = 8;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void YachtCountedAsThrees()
    {
        var actual = new Yacht().CalculateScore(new int[] { 3, 3, 3, 3, 3 }, "threes");
        var expected = 15;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void YachtOf3sCountedAsFives()
    {
        var actual = new Yacht().CalculateScore(new int[] { 3, 3, 3, 3, 3  }, "fives");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Sixes()
    {
        var actual = new Yacht().CalculateScore(new int[] { 2, 3, 4, 5, 6}, "sixes");
        var expected = 6;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void FullHouseTwoSmallThreeBig()
    {
        var actual = new Yacht().CalculateScore(new int[] { 2, 2, 4, 4, 4 }, "full house");
        var expected = 16;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void FullHouseThreeSmallTwoBig()
    {
        var actual = new Yacht().CalculateScore(new int[] { 5, 3, 3, 5, 3 }, "full house");
        var expected = 19;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TwoPairsAreNotAFullHouse()
    {
        var actual = new Yacht().CalculateScore(new int[] { 2, 2, 4, 4, 5 }, "full house");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void YachtIsNotAFullHouse()
    {
        var actual = new Yacht().CalculateScore(new int[] { 2, 2, 2, 2, 2 }, "full house");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void FourOfAKind()
    {
        var actual = new Yacht().CalculateScore(new int[] { 6, 6, 4, 6, 6 }, "four of a kind");
        var expected = 24;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void YachtCanBeScoredAsFourOfAKind()
    {
        var actual = new Yacht().CalculateScore(new int[] { 3, 3, 3, 3, 3 }, "four of a kind");
        var expected = 12;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void FullHouseIsNotFourOfAKind()
    {
        var actual = new Yacht().CalculateScore(new int[] { 3, 3, 3, 5, 5 }, "four of a kind");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void LittleStraight()
    {
        var actual = new Yacht().CalculateScore(new int[] { 3, 5, 4, 1, 2 }, "little straight");
        var expected = 30;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void LittleStraightAsBigStraight()
    {
        var actual = new Yacht().CalculateScore(new int[] { 1, 2, 3, 4, 5 }, "big straight");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void FourInOrderButNotALittleStraight()
    {
        var actual = new Yacht().CalculateScore(new int[] { 1, 1, 2, 3, 4 }, "little straight");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void NoPairsButNotALittleStraight()
    {
        var actual = new Yacht().CalculateScore(new int[] { 1, 2, 3, 4, 6 }, "little straight");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void MinimumIs1NaximumIs5ButNotALittleStraight()
    {
        var actual = new Yacht().CalculateScore(new int[] { 1, 1, 3, 4, 5 }, "little straight");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BigStraight()
    {
        var actual = new Yacht().CalculateScore(new int[] { 4, 6, 2, 5, 3 }, "big straight");
        var expected = 30;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BigStraightAsLittleStraight()
    {
        var actual = new Yacht().CalculateScore(new int[] { 6, 5, 4, 3, 2 }, "little straight");
        var expected = 0;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Choice()
    {
        var actual = new Yacht().CalculateScore(new int[] { 3, 3, 5, 6, 6 }, "choice");
        var expected = 23;
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void YachtAsChoice()
    {
        var actual = new Yacht().CalculateScore(new int[] { 2, 2, 2, 2, 2 }, "choice");
        var expected = 10;
        Assert.AreEqual(expected, actual);
    }

}
