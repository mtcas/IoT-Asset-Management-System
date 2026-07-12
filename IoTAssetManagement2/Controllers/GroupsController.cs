//01110111
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoTAssetManagement2.Models.Entities;
using IoTAssetManagement2.Data;

public class GroupsController : Controller
{
    //Dependancy injection
    private readonly ApplicationDbContext _context;

    public GroupsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: GROUPS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Groups.ToListAsync());
    }

    // GET: GROUPSS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var groups = await _context.Groups
            .FirstOrDefaultAsync(m => m.GroupId == id);
        if (groups == null)
        {
            return NotFound();
        }

        return View(groups);
    }

    // GET: GROUPSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GROUPS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("GroupId,GroupName,ParentGroupId,Description,Devices,InverseParentGroup,ParentGroup")] Groups groups)
    {
        if (ModelState.IsValid)
        {
            _context.Add(groups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(groups);
    }

    // GET: GROUPS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var groups = await _context.Groups.FindAsync(id);
        if (groups == null)
        {
            return NotFound();
        }
        return View(groups);
    }

    // POST: GROUPS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? groupid, [Bind("GroupId,GroupName,ParentGroupId,Description,Devices,InverseParentGroup,ParentGroup")] Groups groups)
    {
        if (groupid != groups.GroupId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(groups);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupsExists(groups.GroupId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(groups);
    }

    // GET: GROUPS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var groups = await _context.Groups
            .FirstOrDefaultAsync(m => m.GroupId == id);
        if (groups == null)
        {
            return NotFound();
        }

        return View(groups);
    }

    // POST: GROUPS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var groups = await _context.Groups.FindAsync(id);
        if (groups != null)
        {
            _context.Groups.Remove(groups);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GroupsExists(int? id)
    {
        return _context.Groups.Any(e => e.GroupId == id);
    }
}