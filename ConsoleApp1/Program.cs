using PgpCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {                
                
                Crip();
                //DEcrip();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro durante a execução do programa: " + ex.Message);
            }
        }

        /// <summary>
        /// Criptografa os arquivos presentes na pasta "Arquivos CRIP" para um determinado cliente.
        /// </summary>
        public static void Crip()
        {
            string pastaArquivos = @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Arquivos\Arquivos CRIP";
            string pastaCriptografados = @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Arquivos\Criptografados\";
            string[] extensoesCriptografar = new string[] { ".out", ".ota", ".inp", ".INP", ".txt", ".gpg"};

            Dictionary<string, string[]> chavesClientes = new Dictionary<string, string[]>
            {
                { "JSC", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\JSC\MAG.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\JSC\RobertoGuisasolaPinachoJSC.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\JSC\Virginia Raluca Croitoru.asc"
                } },
                { "VALID", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\VALID\ABnote GSM.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\VALID\proceso.ficheros.asc"
                } },
                { "1OT", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\1OT\1oT_Ken-Tristan_Peterson.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\1OT\1oT_Marcin_Kulczycki.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\1OT\proceso.ficheros.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\1OT\VALID_TCs.asc"
                } },
                { "GEMALTO", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\GEMALTO\DGC_CVA_V2.asc",
                } },
                { "LinksField", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Criptografia\Public Keys\LinksField\linksfield_pk_brasil.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\JSC\MAG.asc"
                } },
                { "PARETEUM", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Criptografia\Public Keys\PARETEUM\PTSecure.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Criptografia\Public Keys\PARETEUM\Ramy Sayed.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Criptografia\Public Keys\PARETEUM\Ramy.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Criptografia\Public Keys\PARETEUM\Teumpub.asc"
                } },
                { "GD", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\G&D\GSM_Datagen_Brazil.asc"
                } },
                { "WATCHDATA", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Criptografia\Public Keys\WatchData\AdH_Watchdata.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Criptografia\Public Keys\WatchData\lu meng.asc",
                } },
                { "TELIA", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\TELIA\DGC_CVA_V3.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\TELIA\PublicKeyNicolasR.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\TELIA\Teumpub.asc",
                } },
                { "WORKZ", new string[] {
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\WORKZ\Workz_SIMDATA.asc",
                    @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Public Keys\WORKZ\Workz_SIMDATA (739E7C87) – Public.asc",
                } },
            };

            string cliente = "VALID";

            if (chavesClientes.ContainsKey(cliente))
            {
                string[] chavesCliente = chavesClientes[cliente];
                string chavePrivada = @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Telecall Keys\9E0F5632E05C7228F9256899D5085A8ABE69EFB4.asc";
                string senha = "Gi?6ae}a)14Cv&U%&!U#Qp5^j!n+SH@M9_<M7uJluM;s!&L<JkN!fC`#+{q*Wu%";

                CriptografarArquivosRecursivamente(pastaArquivos, pastaCriptografados, extensoesCriptografar, chavesCliente, chavePrivada, senha);
            }
            else
            {
                Console.WriteLine("Cliente não encontrado: " + cliente);
            }
        }

        public static void CriptografarArquivosRecursivamente(string pastaAtual, string pastaCriptografados, string[] extensoesCriptografar, string[] chavesCliente, string chavePrivada, string senha)
        {
            string[] arquivos = Directory.GetFiles(pastaAtual);

            using (PGP pgp = new PGP())
            {
                foreach (string caminhoArquivo in arquivos)
                {
                    string extensaoArquivo = Path.GetExtension(caminhoArquivo);

                    if (extensoesCriptografar.Contains(extensaoArquivo))
                    {
                        string nomeArquivo = Path.GetFileName(caminhoArquivo);
                        string caminhoCriptografado = Path.Combine(pastaCriptografados, nomeArquivo + ".pgp");

                        try
                        {
                            pgp.EncryptFileAndSign(caminhoArquivo, caminhoCriptografado, chavesCliente, chavePrivada, senha);
                            Console.WriteLine("Arquivo criptografado: " + caminhoArquivo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao criptografar o arquivo " + caminhoArquivo + ": " + ex.Message);
                        }
                    }
                }
            }

            string[] subpastas = Directory.GetDirectories(pastaAtual);

            foreach (string subpasta in subpastas)
            {
                string nomeSubpasta = Path.GetFileName(subpasta);
                string novaPastaCriptografados = Path.Combine(pastaCriptografados, nomeSubpasta);

                Directory.CreateDirectory(novaPastaCriptografados);

                try
                {
                    CriptografarArquivosRecursivamente(subpasta, novaPastaCriptografados, extensoesCriptografar, chavesCliente, chavePrivada, senha);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao criptografar arquivos na pasta " + Path.GetFileName(subpasta) + ": " + ex.Message);
                }
            }
        }

        //<summary>
        //Descriptografa todos os arquivos presentes na pasta "Arquivos DECRIP".
        //</summary>
        public static void DEcrip()
        {
            string pastaArquivosDecrip = @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Arquivos\Arquivos DECRIP";
            string pastaDescriptografados = @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Arquivos\Descriptografados\";
            string chavePrivada = @"C:\Users\patrick.oliveira\Desktop\Criptografia\Criptografia\Telecall Keys\9E0F5632E05C7228F9256899D5085A8ABE69EFB4.asc";
            string senha = "Gi?6ae}a)14Cv&U%&!U#Qp5^j!n+SH@M9_<M7uJluM;s!&L<JkN!fC`#+{q*Wu%";

            DescriptografarArquivosRecursivamente(pastaArquivosDecrip, pastaDescriptografados, chavePrivada, senha);
        }

        public static void DescriptografarArquivosRecursivamente(string pastaAtual, string pastaDescriptografados, string chavePrivada, string senha)
        {
            string[] arquivos = Directory.GetFiles(pastaAtual);

            using (PGP pgp = new PGP())
            {
                foreach (string caminhoArquivoDecrip in arquivos)
                {
                    string nomeArquivo = Path.GetFileName(caminhoArquivoDecrip);
                    string caminhoDescriptografado = Path.Combine(pastaDescriptografados, nomeArquivo);

                    try
                    {
                        pgp.DecryptFile(caminhoArquivoDecrip, caminhoDescriptografado, chavePrivada, senha);
                        Console.WriteLine("Arquivo descriptografado: " + nomeArquivo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao descriptografar o arquivo " + nomeArquivo + ": " + ex.Message);
                    }
                }
            }

            string[] subpastas = Directory.GetDirectories(pastaAtual);

            foreach (string subpasta in subpastas)
            {
                string nomeSubpasta = Path.GetFileName(subpasta);
                string novaPastaDescriptografados = Path.Combine(pastaDescriptografados, nomeSubpasta);

                Directory.CreateDirectory(novaPastaDescriptografados);

                try
                {
                    DescriptografarArquivosRecursivamente(subpasta, novaPastaDescriptografados, chavePrivada, senha);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao descriptografar arquivos na pasta " + nomeSubpasta + ": " + ex.Message);
                }
            }
        }
    }
}