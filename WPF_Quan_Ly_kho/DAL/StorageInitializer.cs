using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Quan_Ly_kho.Model;

namespace WPF_Quan_Ly_kho.DAL
{
    public class StorageInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StorageContext>
    {
        protected override void Seed(StorageContext context)
        {
            var units = new List<Unit>
            {
            new Unit{ID=1, DisplayName="Kilogram"},
            new Unit{ID=2, DisplayName="Ton"},
            new Unit{ID=3, DisplayName="Liter"},
            new Unit{ID=4, DisplayName="Block"}
            };

            var supliers = new List<Suplier>
            {
            new Suplier{ID=1, DisplayName="Lu Huu Hoa", Address="TP.HCM", Phone="0123456789", Email="huuhoa@gmail.com", MoreInfo="no", ContractDate=DateTime.Parse("01/01/2023")},
            new Suplier{ID=2, DisplayName="Dinh Hoang Minh Thuan", Address="Binh Duong", Phone="1234509876", Email="minhthuan@gmail.com", MoreInfo="no", ContractDate=DateTime.Parse("01/01/2023")},
            new Suplier{ID=3, DisplayName="Ngo Van Toan", Address="TP.HCM", Phone="1234567890", Email="toanngo@gmail.com", MoreInfo="no", ContractDate=DateTime.Parse("01/01/2023")}
            };
            supliers.ForEach(s => context.Supliers.Add(s));
            context.SaveChanges();

            units.ForEach(s => context.Units.Add(s));
            context.SaveChanges();

            var inputs = new List<Input>
            {
            new Input{ID=1,DateInput=DateTime.Parse("2023-06-08")},
            new Input{ID=2,DateInput=DateTime.Parse("2023-06-09")},
            };
            inputs.ForEach(s => context.Inputs.Add(s));
            context.SaveChanges();

            var outputs = new List<Output>
            {
            new Output{ID=1, DateOutput=DateTime.Parse("2023-06-08")},
            new Output{ID=2, DateOutput=DateTime.Parse("2023-06-09")},
            };
            outputs.ForEach(s => context.Outputs.Add(s));
            context.SaveChanges();

            var customers = new List<Customer>
            {
            new Customer{ID=1, DisplayName="Nguyen Anh Thanh", Address="TP.HCM", Phone="0123456789", Email="anhthanh@gmail.com", MoreInfo="no", ContractDate=DateTime.Parse("01/01/2023")},
            new Customer{ID=2, DisplayName="Dang Duy Thong", Address="Binh Duong", Phone="1234509876", Email="duythong@gmail.com", MoreInfo="no", ContractDate=DateTime.Parse("01/01/2023")},
            new Customer{ID=3, DisplayName="Ho Thanh Hau", Address="TP.HCM", Phone="1234567890", Email="hauho@gmail.com", MoreInfo="no", ContractDate=DateTime.Parse("01/01/2023")}
            };
            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

            var userroles = new List<UserRole>
            {
                new UserRole{ID=1, DisplayName="Admin"},
                new UserRole{ID=2, DisplayName="Boss"},
                new UserRole{ID=3, DisplayName="Staff"}
            };
            userroles.ForEach(s => context.UserRoles.Add(s));
            context.SaveChanges();

            var users = new List<User>
            {
                new User{ID=1, DisplayName="Nguyen Anh Duy", Username="Duy", Password="123", IDRole=1},
                new User{ID=2, DisplayName="Lu Huu Thuan", Username="Thuan", Password="123", IDRole=2},
                new User{ID=3, DisplayName="Nguyen Nhat Quang", Username="Quang", Password="123", IDRole=3}
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var materials = new List<Material>
            {
                new Material{ID=1, DisplayName="Xi Mang", IDUnit=1, IDSuplier=1},
                new Material{ID=2, DisplayName="Gach", IDUnit=4, IDSuplier=2},
                new Material{ID=3, DisplayName="Cat", IDUnit=2, IDSuplier=3}
            };
            materials.ForEach(s => context.Materials.Add(s));
            context.SaveChanges();

            var inputinfos = new List<InputInfo>
            {
            new InputInfo{ID=1,IDInput=1, IDMaterial=1, Count=10, InputPrice=100000 },
            new InputInfo{ID=2,IDInput=2, IDMaterial=2, Count=100, InputPrice=1000000 },
            };
            inputinfos.ForEach(s => context.InputInfos.Add(s));
            context.SaveChanges();

            var outputinfos = new List<OutputInfo>
            {
            new OutputInfo{ID=1,IDOutput=1, IDMaterial=1, IDCustomer=1, Count=5, OutputPrice=50000 },
            new OutputInfo{ID=2,IDOutput=2, IDMaterial=2, IDCustomer=2, Count=25, OutputPrice=250000 },
            };
            outputinfos.ForEach(s => context.OutputInfos.Add(s));
            context.SaveChanges();
        }
    }
}
