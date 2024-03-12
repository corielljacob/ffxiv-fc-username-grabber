using NetStone;
using NetStone.Model.Parseables.FreeCompany.Members;

namespace UsernameGrabber
{
    public class Program
    {
        private static async Task Main()
        {
            await RetrieveFreeCompanyMembers();
        }

        private static async Task RetrieveFreeCompanyMembers()
        {
            try
            {
                var lodestoneClient = await LodestoneClient.GetClientAsync();
                int pagesOfMembers = 1;

                if (File.Exists(".\\playersClean.txt"))
                    File.Delete(".\\playersClean.txt");

                for(int page = 1; page <= pagesOfMembers; page++)
                {
                    var freeCompanyMembers = await lodestoneClient.GetFreeCompanyMembers("9229142273877457819", page);

                    if (AnyValueIsNull(freeCompanyMembers))
                    {
                        Console.WriteLine("Something went wrong. Retry or contact the donut.");
                        return;
                    }

                    pagesOfMembers = freeCompanyMembers.NumPages;
                    var memberList = freeCompanyMembers?.Members;
                    foreach (var member in memberList)
                    {
                        File.AppendAllText(".\\playersClean.txt", $"{member.Name}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Retry or contact the donut.");
                Console.WriteLine(ex.Message);
            }
        }

        private static bool AnyValueIsNull(FreeCompanyMembers? freeCompanyMembers)
        {
            if(freeCompanyMembers == null || freeCompanyMembers.Members == null)
                return true;
            else
                return false;
        }
    }
}



