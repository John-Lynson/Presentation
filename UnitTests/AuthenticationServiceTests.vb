Imports System.Threading.Tasks
Imports Xunit
Imports Moq
Imports Core.Models
Imports Core.Services
Imports Core.Repositories
Imports Microsoft.AspNetCore.Identity

Public Class AuthenticationServiceTests
    Private _userRepositoryMock As Mock(Of IUserRepository)
    Private _passwordHasherMock As Mock(Of IPasswordHasher(Of User))
    Private _authenticationService As AuthenticationService

    Public Sub New()
        _userRepositoryMock = New Mock(Of IUserRepository)()
        _passwordHasherMock = New Mock(Of IPasswordHasher(Of User))()
        _authenticationService = New AuthenticationService(_userRepositoryMock.Object, _passwordHasherMock.Object, "TestJWTKey")
    End Sub

    <Fact>
    Public Async Function RegisterAsync_UserDoesNotExist_CreatesUser() As Task
        ' Arrange
        Dim newUser As New User() With {.Email = "test@test.com"}
        _userRepositoryMock.Setup(Function(x) x.GetUserByEmailAsync(It.IsAny(Of String))).Returns(Task.FromResult(Of User)(Nothing))

        ' Act
        Dim result = Await _authenticationService.RegisterAsync(newUser, "testPassword")

        ' Assert
        _userRepositoryMock.Verify(Function(x) x.AddUserAsync(It.IsAny(Of User)), Times.Once)
        Assert.True(result.IsAuthenticated)
    End Function

    <Fact>
    Public Async Function AuthenticateAsync_CorrectCredentials_ReturnsAuthenticatedUser() As Task
        ' Arrange
        Dim testUser As New User() With {.Email = "test@test.com", .PasswordHash = "hashedPassword"}
        _userRepositoryMock.Setup(Function(x) x.GetUserByEmailAsync(It.IsAny(Of String))).Returns(Task.FromResult(testUser))
        _passwordHasherMock.Setup(Function(x) x.VerifyHashedPassword(It.IsAny(Of User), It.IsAny(Of String), It.IsAny(Of String))).Returns(PasswordVerificationResult.Success)

        ' Act
        Dim result = Await _authenticationService.AuthenticateAsync(New AuthenticateRequest() With {.Email = "test@test.com", .Password = "testPassword"})

        ' Assert
        Assert.True(result.IsAuthenticated)
    End Function
End Class
