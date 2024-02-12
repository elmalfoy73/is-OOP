using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    private IsuService _isuService = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        GroupName m32061 = new GroupName("M32061");
        Group group1 = _isuService.AddGroup(m32061);
        Student student = _isuService.AddStudent(group1, "Tregubovich Elizabeth");

        Assert.Contains(student, group1.Students);
        Assert.Equal(group1.Name, student.Group.Name);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        GroupName groupName = new GroupName("M32061");
        Group group = _isuService.AddGroup(groupName);
        Assert.Throws<IsuException>(() =>
        {
            for (int i = 0; i < 33; i++)
            {
                _isuService.AddStudent(group, "x");
            }
        });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<IsuException>(() =>
        {
            new GroupName("M3500");
        });
        Assert.Throws<IsuException>(() =>
        {
            new GroupName("bestGroup");
        });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        GroupName m32061 = new GroupName("M32061");
        GroupName m32001 = new GroupName("M32001");
        Group group1 = _isuService.AddGroup(m32061);
        Group group2 = _isuService.AddGroup(m32001);
        Student student = _isuService.AddStudent(group1, "Tregubovich Elizabeth");
        _isuService.ChangeStudentGroup(student, group2);
        Assert.Equal(group2, student.Group);
    }
}