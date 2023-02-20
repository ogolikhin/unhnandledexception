using Microsoft.AspNetCore.Mvc;

namespace UnhandledException.Controllers;

[ApiController]
[Route("[controller]")]
public class MicrosoftAuthController: ControllerBase
{
  private readonly TestTaskRun _testTaskRun;

  public MicrosoftAuthController(TestTaskRun testTaskRun)
  {
    _testTaskRun = testTaskRun;
  }

  [HttpGet]
  public async Task<IActionResult> GetTest()
  {
    await _testTaskRun.Run();
    return Ok();
  }
}

public class TestTaskRun
{
  public async Task Run()
  {
    var intern = new TestTaskRunInternal();
    intern.SomethingHappens += async (s, e) => await Intern_SomethingHappens(s, e);
    var task = Task.Run(intern.Run);
  }

  private async Task Intern_SomethingHappens(object? sender, EventArgs? e)
    => throw new NotImplementedException();
}

class TestTaskRunInternal
{
  internal event EventHandler<EventArgs>? SomethingHappens;
  public async Task Run()
  {
    await Task.Delay(2000);

    SomethingHappens.Invoke(this, new EventArgs());
  }
}
