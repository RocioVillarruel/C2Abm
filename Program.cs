var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var teams = new List<Team>();

var index=1;
var team=new Team ("Barcelona");
team.id=index; 
teams.Add(team);

//GET ALL
app.MapGet("/api/teams", () => teams);

//GET
app.MapGet("/api/teams/{id}", (int id) => {
    var team = teams.FirstOrDefault(x => x.id == id);
    return team; 
});

//CREATE (POST)
app.MapPost("api/teams", (Team teamImput)=>
{
    var exist= teams.FirstOrDefault(x=>x.name.ToLower() == teamImput.name.ToLower());
    if (exist!= null)
    {
        return false;
    }

    index=index+1;
    teamImput.id=index;
    teams.Add(teamImput);
    return true;
});

//EDIT (PUT)
app.MapPut("api/teams", (Team teamImput)=>
{
    var team= teams.FirstOrDefault(x=>x.id== teamImput.id);
    if (team== null)
    {
        return false;
    }

    teams.Remove(team);
    teams.Add(teamImput);
    return true;
});

//DELETE
app.MapDelete("/api/teams/{id}", (int id) =>
{
    var team = teams.FirstOrDefault(x => x.id == id);
    if (team == null)
    {
        return false;
    }
    teams.Remove(team);
    return true;
});


app.Run();
