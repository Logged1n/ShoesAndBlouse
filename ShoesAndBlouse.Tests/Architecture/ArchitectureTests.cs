using FluentAssertions;
using NetArchTest.Rules;

namespace ShoesAndBlouse.Tests.Architecture;

public class ArchitectureTests
{
    private const string DomainNamespace = "ShoesAndBlouse.Domain";
    private const string ApplicationNamespace = "ShoesAndBlouse.Application";
    private const string InfrastructureNamespace = "ShoesAndBlouse.Infrastructure";
    private const string PresentationNamespace = "ShoesAndBlouse.API";
    
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var domainAssembly = typeof(ShoesAndBlouse.Domain.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace
        };
        
        //Act
        var result = Types
            .InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();
        
        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        //Arrange
        var applicationAssembly = typeof(Application.DependencyInjection).Assembly;
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace
        };
        
        //Act
        var result = Types
            .InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();
        
        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnPresentation()
    {
        //Arrange
        var assembly = typeof(Infrastructure.DependencyInjection).Assembly;
        
        //Act
        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn(PresentationNamespace)
            .GetResult();
        
        //Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Presentation_Should_Not_HaveDependencyOnInfrastructure()
    {
        //Arrange
        var assembly = typeof(API.AssemblyReference).Assembly;
        
        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .DoNotHaveName("Program") //Exclude the AddInfrastructure Method in Program.cs from checking
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}