﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HrApplyApp.Models;

namespace HRApplyApp.Data
{
    public class PolicyRepository
    {
        private string _fileName
        {
            get { return RootPath + "DataFiles\\policies.txt"; }
        }

        public string RootPath { get; set; }

        public List<Policy> GetAll()
        {
            List<Policy> AllPolicies = new List<Policy>();

            if (File.Exists(_fileName))
            {
                var reader = File.ReadAllLines(_fileName);

                // read the header

                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var Policy = new Policy()
                    {
                        PolicyID = int.Parse(columns[0]),
                        PolicyName = columns[1],
                        PolicyDescription = columns[2],
                        Category = columns[3],
                    };

                    AllPolicies.Add(Policy);
                }
            }



            return AllPolicies;
        }

        public List<PolicyCategory> GetPoliciesByCategory(List<Policy> policyList)
        {
            PolicyCategory polcat = new PolicyCategory();
            List<PolicyCategory> polcatlist = new List<PolicyCategory>();
            foreach (var policy in policyList)
            {
                foreach (var singlepolicy in policyList)
                {
                    var catstring = singlepolicy.Category;
                    if (polcatlist.Any(m=>m.PolicyCategoryName == catstring))
                    {
                        
                    }
                }

            }
            return polcatlist;
        }

        public void Add(Policy newPolicy)
        {
            // ternary operator is saying:
            // if there are any contacts return the max contact id and add 1 to set our new contact id
            // else set to 1
            newPolicy.PolicyID = (GetAll().Any()) ? GetAll().Max(p => p.PolicyID) + 1 : 1;

            var Policies = GetAll();
            Policies.Add(newPolicy);

            WriteFile(newPolicy);
        }

        public void WriteFile(Policy pol)
        {

            using (var writer = File.AppendText(_fileName))
            {

                writer.WriteLine(
                    String.Format("{0},{1},{2},{3}",
                        pol.PolicyID, pol.PolicyName, pol.PolicyDescription, pol.Category));


            }
        }
    }
}