using Kae.CIM;
using Kae.CIM.MetaModel.CIMofCIM;
using Kae.XTUML.Tools.CIModelResolver;
using System;

namespace SampleModelGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLine = new CommandLine();

            if (commandLine.Parse(args))
            {
                Console.WriteLine($"Meta Model File : {commandLine.MetaModelFilePath}");
                Console.WriteLine($"Data Type Def File : {commandLine.DataTypeDefFilePath}");
                if (commandLine.GenerateFWLib)
                {
                    Console.WriteLine("Meta Model Framework Generation : yes");
                    Console.WriteLine($"  Generation Folder : {commandLine.GenFolderPath}");
                }
                if (!string.IsNullOrEmpty(commandLine.InstancesFile))
                {
                    Console.WriteLine($"Instances File Path : {commandLine.InstancesFile}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(commandLine.GetCommandLine());
                return;
            }

            var resolver = new ConceptualInformationModelResolver(commandLine.MetaModelFilePath);
            try
            {
                Console.WriteLine($"Loading OOA of OOA model... @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                resolver.LoadOOAofOOA(commandLine.DataTypeDefFilePath);
                Console.WriteLine($"Loaded.  @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");

                if (commandLine.GenerateFWLib && !string.IsNullOrEmpty(commandLine.GenFolderPath))
                {
                    Console.WriteLine($"Generating Meta Model Framework Library...  @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                    resolver.GenerateCIMFramework(commandLine.GenFolderPath).Wait();
                    Console.WriteLine($"Generated.   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                }
                if (!string.IsNullOrEmpty(commandLine.InstancesFile))
                {
                    Console.WriteLine($"Loading instances...   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                    resolver.LoadCIInstances(commandLine.InstancesFile, true);
                    Console.WriteLine($"Loaded.   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");

                    ShowLoadedModel(resolver.ModelRepository);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var ciinstances = resolver.ModelRepository.GetDomainCIClasses(CIMOOAofOOADomainName);
            Console.WriteLine($"Count - {ciinstances.Keys.Count}");
        }

        private static readonly string CIMOOAofOOADomainName= "OOAofOOA";

        private static void ShowLoadedModel(CIModelRepository modelRepository)
        {
            var o_obj_set = modelRepository.GetCIInstances(CIMOOAofOOADomainName, "O_OBJ");
            foreach (var o_obj_abst in o_obj_set)
            {
                var o_obj_inst = (CIMClassO_OBJ)o_obj_abst;
                Console.WriteLine($"{o_obj_inst.ClassName}:Name={o_obj_inst.Attr_Name}");
                var o_iobj_set = o_obj_inst.LinkedFromR101();
                foreach (var o_iobj_inst in o_iobj_set)
                {
                    Console.Write($"{o_iobj_inst.ClassName}:Name={o_iobj_inst.Attr_Obj_Name}");
                    var r_oir_r202_set = o_iobj_inst.LinkedFromR202();
                }
                var o_attr_set = o_obj_inst.LinkedFromR102();
                foreach(var o_attr_inst in o_attr_set)
                {
                    Console.WriteLine($"{o_attr_inst.ClassName}:Name={o_attr_inst.Attr_Name}");
                    var o_obj_r102_inst = o_attr_inst.LinkedToR102();
                    var o_attr_r103_Succeeds_inst = o_attr_inst.LinkedFromR103Succeeds();
                    var o_attr_r103_Predecedes_inst = o_attr_inst.LinkedToR103Precedes();
                    var s_dt_inst = o_attr_inst.LinkedToR114();
                    var o_oida_set = o_attr_inst.LinkedOneSideR105();
                    var s_dim_set = o_attr_inst.LinkedFromR120();
                    var v_avl_set = o_attr_inst.LinkedFromR806();
                    var v_sl_set = o_attr_inst.LinkedFromR812();
                    var sq_av_set = o_attr_inst.LinkedFromR938();
                    var te_attr_inst = o_attr_inst.LinkedFromR2033();
                    var i_avl_set = o_attr_inst.LinkedFromR2910();

                    var sub_class_inst = o_attr_inst.SubClassR106();
                    if (sub_class_inst != null)
                    {
                        Console.WriteLine($"Super Class of ${sub_class_inst.GetType().Name}");
                    }
                }
                var o_id_set = o_obj_inst.LinkedFromR104();
                var o_tfr_set = o_obj_inst.LinkedFromR115();
                var s_irdt_set = o_obj_inst.LinkedFromR123();
                var r_oir_set = o_obj_inst.LinkedOneSideR201();
                var sm_ism_inst = o_obj_inst.LinkedFromR518();
                var sm_asm_inst = o_obj_inst.LinkedFromR519();
                var act_for_set = o_obj_inst.LinkedFromR670();
                var act_cr_set = o_obj_inst.LinkedFromR671();
                var act_cnv_set = o_obj_inst.LinkedFromR672();
                var act_fiw_set = o_obj_inst.LinkedFromR676();
                var act_fio_set = o_obj_inst.LinkedFromR677();
                var act_lnk_set= o_obj_inst.LinkedFromR678();
                var v_int_set = o_obj_inst.LinkedFromR818();
               var v_ins_set =  o_obj_inst.LinkedFromR819();
                var sq_cip_set = o_obj_inst.LinkedFromR934();
                var sq_cp_set = o_obj_inst.LinkedFromR939();
                var te_class_inst = o_obj_inst.LinkedFromR2019();
            }

            var s_sys_set = modelRepository.GetCIInstances(CIMOOAofOOADomainName, "S_SYS");
            foreach(var s_sys_abst in s_sys_set)
            {
                var s_sys_inst = (CIMClassS_SYS)s_sys_abst;
                Console.WriteLine($"{s_sys_inst.ClassName}:Name={s_sys_inst.Attr_Name}");
                var ep_pkg_r1401_set =  s_sys_inst.LinkedFromR1401();
                foreach(var ep_pkg_abst in ep_pkg_r1401_set)
                {
                    ShowEP_PKG((CIMClassEP_PKG)ep_pkg_abst);
                }
                var ep_pkg_r1405_set = s_sys_inst.LinkedFromR1405();
                foreach(var ep_pkg_abst in ep_pkg_r1405_set)
                {
                    ShowEP_PKG((CIMClassEP_PKG)ep_pkg_abst);
                }
                var te_sys_abst = s_sys_inst.LinkedFromR2018();
                var g_eis_set = s_sys_inst.LinkedOneSideR9100();
            }
        }
        private static void ShowEP_PKG(CIMClassEP_PKG inst)
        {
            Console.WriteLine($"{inst.ClassName}:Name={inst.Attr_Name}");
            var i_exe_instt = inst.LinkedFromR2970();
            var pe_pe_set = inst.LinkedFromR8000();
            var pe_srs_set = inst.LinkedFromR8005();
            var sq_pp_set = inst.LinkedFromR956();
            var ep_pkg_ref_r1402_IsReferencedBy_set = inst.LinkedOneSideR1402IsReferencedBy();
            var pe_vis_set = inst.LinkedOneSideR8002();
            var ep_pkg_ref_r1402_RefersTo_set = inst.LinkedOtherSideR1402RefersTo();
            var s_sys_r1401_inst = inst.LinkedToR1401();
            var s_sys_r1405_inst = inst.LinkedToR1405();
        }

        private static void ShowPE_PE(CIMClassPE_PE inst)
        {
            Console.WriteLine($"{inst.ClassName}:Package_ID={inst.Attr_Package_ID},Element_ID={inst.Attr_Element_ID}");
            var act_vie_set = inst.LinkedOneSideR640();
            var pe_cvs_set= inst.LinkedOneSideR8004();
            var pe_vis_set= inst.LinkedOtherSideR8002();
            var g_eis_set = inst.LinkedOtherSideR9100();
            var ep_pkg_inst= inst.LinkedToR8000();
            var c_c_inst = inst.LinkedToR8003();
        }
    }

    class CommandLine
    {
        public string MetaModelFilePath { get; set; }
        public string GenFolderPath { get; set; }
        public bool GenerateFWLib { get; set; }
        public string InstancesFile { get; set; }
        public bool BuildFWLib { get; set; }
        public string DataTypeDefFilePath { get; set; }

        public bool Parse(string[] args)
        {
            bool result = true;
            MetaModelFilePath = null;
            GenFolderPath = null;
            GenerateFWLib = false;
            InstancesFile = null;
            BuildFWLib = false;
            int index = 0;
            while (index < args.Length)
            {
                if (args[index] == "-m" || args[index] == "--model")
                {
                    if (++index < args.Length)
                    {
                        MetaModelFilePath = args[index];
                    }
                }
                else if (args[index] == "-o" || args[index] == "--out")
                {
                    if (++index < args.Length)
                    {
                        GenFolderPath = args[index];
                    }
                }
                else if (args[index] == "-gf" || args[index] == "--gen-fwlib")
                {
                    if (args[index] == "-gf")
                    {
                        if (++index < args.Length)
                        {
                            if (args[index] == "yes")
                            {
                                GenerateFWLib = true;
                            }
                        }
                    }
                    else
                    {
                        GenerateFWLib = true;
                    }
                }
                else if (args[index] == "-b" || args[index] == "--build")
                {
                    BuildFWLib = true;
                }
                else if (args[index] == "-li" || args[index] == "--load-instances")
                {
                    if (++index < args.Length)
                    {
                        InstancesFile = args[index];
                    }
                }
                else if (args[index] == "-dt" || args[index] == "--data-type")
                {
                    if (++index < args.Length)
                    {
                        DataTypeDefFilePath = args[index];
                    }
                }
                index++;
            }
            if (string.IsNullOrEmpty(MetaModelFilePath) || string.IsNullOrEmpty(DataTypeDefFilePath))
            {
                result = false;
            }
            if (GenerateFWLib && string.IsNullOrEmpty(GenFolderPath))
            {
                result = false;
            }
            if (GenerateFWLib == false)
            {
                BuildFWLib = false;
            }
            return result;
        }

        public string GetCommandLine()
        {
            return "--model model_file_path --data-type datatype_def_file_path [--gen-fwlib|--gf (yes|no) --out gen_folder_path [-b|--build]] [-li|--looad-instanes instances_file_or_directory_path]";
        }
    }

}
