using ClaimDigitalize.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClaimDigitalize.Services
{
    public class ClaimService
    {
        private ClaimIntimationVM GetAccident(ClaimVM claimVM)
        {
            ClaimIntimationVM claimIntimation = new ClaimIntimationVM();
            IntimationQuestionAnswerVM intimationQusAns = new IntimationQuestionAnswerVM();
            WorkshopVM workshop = new WorkshopVM();

            try
            {
                claimIntimation.ClaimId = claimVM.ClaimId;
                bool containsOtherWorkshop = false;

                if (claimVM.WorkshopCollection != null)
                {
                    workshop = claimVM.WorkshopCollection.LastOrDefault();
                    containsOtherWorkshop = claimVM.WorkshopCollection.Any(x => x.WorkshopId == "0");
                }

                if (claimVM.ClaimId == null)
                {
                    claimIntimation.ClaimType_Id = Convert.ToInt32(claimVM.ClaimType);
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.City_Id = Convert.ToInt32(claimVM.IncidentCity);
                    claimIntimation.Area_Id = Convert.ToInt32(claimVM.IncidentArea);
                    claimIntimation.CurrentCity_Id = Convert.ToInt32(claimVM.CurrentCity);
                    claimIntimation.CurrentArea_Id = Convert.ToInt32(claimVM.CurrentArea);
                    claimIntimation.IncidentDate = Convert.ToDateTime(claimVM.IncidentDate.Replace('-', ' '));
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.VehicleId = Convert.ToInt32(claimVM.Salesformdetailid);
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.ThirdPartytag = false;
                    claimIntimation.ThirdPartyContact = null;
                    claimIntimation.ThirdPartyRemarks = null;
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.DamageParts = claimVM.Damageparts;
                    claimIntimation.WorkshopCollection = claimVM.WorkshopCollection;
                    claimIntimation.VehicleAvailablity = claimVM.VehicleAvailablity;
                    claimIntimation.UserId = claimVM.UserId;
                    claimIntimation.IntimationQusAns = new List<IntimationQuestionAnswerVM>();
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;

                    if (containsOtherWorkshop)
                    {
                        foreach (WorkshopVM _workshop in claimIntimation.WorkshopCollection)
                        {
                            if (_workshop.WorkshopId == "0")
                            {
                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "WorkshopType",
                                    Answer = _workshop.WorkshopType
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);

                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "Workshop",
                                    Answer = _workshop.WorkshopId
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);

                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "WokshopName",
                                    Answer = _workshop.WorkshopName
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);

                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "VehicleAvailablity",
                                    Answer = _workshop.VehicleAvailablity
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);
                            }
                        }
                    }

                    if (workshop != null)
                    {
                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "WorkshopType",
                            Answer = workshop.WorkshopType
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);

                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "Workshop",
                            Answer = workshop.WorkshopId
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);

                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "WokshopName",
                            Answer = workshop.WorkshopName
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);

                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "VehicleAvailablity",
                            Answer = workshop.VehicleAvailablity
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);
                    }

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IsRequireTowTruck",
                        Answer = claimVM.IsRequireTowTruck
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IsRequireRentaCar",
                        Answer = claimVM.IsRequireRentaCar
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "HeavyDamagePart",
                        Answer = claimVM.HeavyDamagePart
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "HeavyDamagePartRemarks",
                        Answer = claimVM.HeavyDamagePartRemarks
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);
                }
                else
                {
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.WorkshopCollection = claimVM.WorkshopCollection;
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.VehicleAvailablity = claimVM.VehicleAvailablity;
                    claimIntimation.DamageParts = claimVM.Damageparts;
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.ClaimType_Id = Convert.ToInt32(claimVM.ClaimType);
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;

                    List<IntimationQuestionAnswerVM> intimationQusAnsLst = CheckClaimExists(Convert.ToInt32(claimVM.ClaimId));

                    if (intimationQusAnsLst.Count <= 0)
                    {
                        return claimIntimation;
                    }

                    foreach (IntimationQuestionAnswerVM item in intimationQusAnsLst)
                    {

                        if (containsOtherWorkshop)
                        {
                            foreach (WorkshopVM _workshop in claimIntimation.WorkshopCollection)
                            {
                                if (_workshop.WorkshopId == "0")
                                {
                                    switch (item.Question_Id)
                                    {
                                        case "WorkshopType":
                                            item.Answer = workshop.WorkshopType;
                                            break;
                                        case "Workshop":
                                            item.Answer = workshop.WorkshopId;
                                            break;
                                        case "WorkshopName":
                                            item.Answer = workshop.WorkshopName;
                                            break;
                                        case "VehicleAvailablity":
                                            item.Answer = workshop.VehicleAvailablity;
                                            break;
                                    }
                                }
                            }
                        }

                        if (workshop != null)
                        {
                            switch (item.Question_Id)
                            {
                                case "WorkshopType":
                                    item.Answer = workshop.WorkshopType;
                                    break;
                                case "Workshop":
                                    item.Answer = workshop.WorkshopId;
                                    break;
                                case "WorkshopName":
                                    item.Answer = workshop.WorkshopName;
                                    break;
                                case "VehicleAvailablity":
                                    item.Answer = workshop.VehicleAvailablity;
                                    break;
                            }
                        }
                    }

                    claimIntimation.IntimationQusAns = intimationQusAnsLst;
                }

            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return claimIntimation;
        }

        private ClaimIntimationVM GetTheftSnatch(ClaimVM claimVM)
        {
            ClaimIntimationVM claimIntimation = new ClaimIntimationVM();

            try
            {
                claimIntimation.ClaimId = claimVM.ClaimId;

                if (claimVM.ClaimId == null)
                {
                    claimIntimation.ClaimType_Id = Convert.ToInt32(claimVM.ClaimType);
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                    claimIntimation.IncidentDate = Convert.ToDateTime(claimVM.IncidentDate.Replace('-', ' '));
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.VehicleId = Convert.ToInt32(claimVM.Salesformdetailid);
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.ThirdPartytag = false;
                    claimIntimation.ThirdPartyContact = null;
                    claimIntimation.ThirdPartyRemarks = null;
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.DamageParts = claimVM.Damageparts;
                    claimIntimation.UserId = claimVM.UserId;
                    claimIntimation.Area_Id = Convert.ToInt32(claimVM.IncidentArea);
                    claimIntimation.City_Id = Convert.ToInt32(claimVM.IncidentCity);
                    claimIntimation.CurrentArea_Id = Convert.ToInt32(claimVM.CurrentArea);
                    claimIntimation.CurrentCity_Id = Convert.ToInt32(claimVM.CurrentCity);
                    claimIntimation.IntimationQusAns = new List<IntimationQuestionAnswerVM>();
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;

                    IntimationQuestionAnswerVM intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IntimateService",
                        Answer = claimVM.IntimateService
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "ComplaintNo",
                        Answer = claimVM.ComplaintNo
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "TrakkerInstall",
                        Answer = claimVM.TrakkerInstall
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "TrackerCompanyName",
                        Answer = claimVM.TrackerCompanyName
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IsRequireTowTruck",
                        Answer = claimVM.IsRequireTowTruck
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IsRequireRentaCar",
                        Answer = claimVM.IsRequireRentaCar
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "HeavyDamagePart",
                        Answer = claimVM.HeavyDamagePart
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "HeavyDamagePartRemarks",
                        Answer = claimVM.HeavyDamagePartRemarks
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);
                }
                else
                {
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.Remarks = claimVM.Remarks;
                    claimIntimation.WorkshopId = "-1";
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.VehicleAvailablity = string.Empty;
                    claimIntimation.DamageParts = claimVM.Damageparts;
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;

                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return claimIntimation;
        }

        private ClaimIntimationVM GetAccessoriesTheft(ClaimVM claimVM)
        {
            ClaimIntimationVM claimIntimation = new ClaimIntimationVM();

            try
            {
                claimIntimation.ClaimId = claimVM.ClaimId;
                bool containsOtherWorkshop = claimVM.WorkshopCollection.Any(x => x.WorkshopId == "0");
                WorkshopVM workshop = claimVM.WorkshopCollection.LastOrDefault();
                IntimationQuestionAnswerVM intimationQusAns = new IntimationQuestionAnswerVM();

                if (claimVM.ClaimId == null)
                {
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                    claimIntimation.City_Id = Convert.ToInt32(claimVM.IncidentCity);
                    claimIntimation.Area_Id = Convert.ToInt32(claimVM.IncidentArea);
                    claimIntimation.CurrentCity_Id = Convert.ToInt32(claimVM.CurrentCity);
                    claimIntimation.CurrentArea_Id = Convert.ToInt32(claimVM.CurrentArea);
                    claimIntimation.IncidentDate = Convert.ToDateTime(claimVM.IncidentDate.Replace('-', ' '));
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.VehicleId = Convert.ToInt32(claimVM.Salesformdetailid);
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.ThirdPartytag = false;
                    claimIntimation.ThirdPartyContact = null;
                    claimIntimation.ThirdPartyRemarks = null;
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.UserId = claimVM.UserId;
                    claimIntimation.DamageParts = claimVM.Damageparts;
                    claimIntimation.WorkshopCollection = claimVM.WorkshopCollection;
                    claimIntimation.VehicleAvailablity = claimVM.VehicleAvailablity;
                    claimIntimation.ClaimType_Id = Convert.ToInt32(claimVM.ClaimType);
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;

                    claimIntimation.IntimationQusAns = new List<IntimationQuestionAnswerVM>();

                    if (containsOtherWorkshop)
                    {
                        foreach (WorkshopVM _workshop in claimIntimation.WorkshopCollection)
                        {
                            if (_workshop.WorkshopId == "0")
                            {
                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "WorkshopType",
                                    Answer = _workshop.WorkshopType
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);

                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "Workshop",
                                    Answer = _workshop.WorkshopId
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);

                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "WokshopName",
                                    Answer = _workshop.WorkshopName
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);

                                intimationQusAns = new IntimationQuestionAnswerVM
                                {
                                    Question_Id = "VehicleAvailablity",
                                    Answer = _workshop.VehicleAvailablity
                                };

                                claimIntimation.IntimationQusAns.Add(intimationQusAns);
                            }
                        }
                    }

                    if (workshop != null)
                    {
                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "WorkshopType",
                            Answer = workshop.WorkshopType
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);

                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "Workshop",
                            Answer = workshop.WorkshopId
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);

                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "WokshopName",
                            Answer = workshop.WorkshopName
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);

                        intimationQusAns = new IntimationQuestionAnswerVM
                        {
                            Question_Id = "VehicleAvailablity",
                            Answer = workshop.VehicleAvailablity
                        };

                        claimIntimation.IntimationQusAns.Add(intimationQusAns);
                    }

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "HeavyDamagePart",
                        Answer = claimVM.HeavyDamagePart
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "HeavyDamagePartRemarks",
                        Answer = claimVM.HeavyDamagePartRemarks
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);
                }
                else
                {
                    List<IntimationQuestionAnswerVM> intimationQusAnsLst = CheckClaimExists(Convert.ToInt32(claimVM.ClaimId));
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.WorkshopCollection = claimVM.WorkshopCollection;
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.VehicleAvailablity = claimVM.VehicleAvailablity;
                    claimIntimation.DamageParts = claimVM.Damageparts;
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.ClaimType_Id = Convert.ToInt32(claimVM.ClaimType);
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;

                    if (intimationQusAnsLst.Count <= 0)
                    {
                        return claimIntimation;
                    }

                    foreach (IntimationQuestionAnswerVM item in intimationQusAnsLst)
                    {
                        if (containsOtherWorkshop)
                        {
                            foreach (WorkshopVM _workshop in claimIntimation.WorkshopCollection)
                            {
                                if (_workshop.WorkshopId == "0")
                                {
                                    switch (item.Question_Id)
                                    {
                                        case "WorkshopType":
                                            item.Answer = workshop.WorkshopType;
                                            break;
                                        case "Workshop":
                                            item.Answer = workshop.WorkshopId;
                                            break;
                                        case "WorkshopName":
                                            item.Answer = workshop.WorkshopName;
                                            break;
                                        case "VehicleAvailablity":
                                            item.Answer = workshop.VehicleAvailablity;
                                            break;
                                    }
                                }
                            }
                        }

                        if (workshop != null)
                        {
                            switch (item.Question_Id)
                            {
                                case "WorkshopType":
                                    item.Answer = workshop.WorkshopType;
                                    break;
                                case "Workshop":
                                    item.Answer = workshop.WorkshopId;
                                    break;
                                case "WorkshopName":
                                    item.Answer = workshop.WorkshopName;
                                    break;
                                case "VehicleAvailablity":
                                    item.Answer = workshop.VehicleAvailablity;
                                    break;
                            }
                        }
                    }

                    claimIntimation.IntimationQusAns = intimationQusAnsLst;
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return claimIntimation;
        }

        private ClaimIntimationVM GetThirdPartyCoverage(ClaimVM claimVM)
        {
            ClaimIntimationVM claimIntimation = new ClaimIntimationVM();

            try
            {
                if (claimVM.ClaimId != null)
                {
                    claimIntimation.WorkshopId = "0";
                    claimIntimation.WorkshopName = null;
                    claimIntimation.WorkshopType = null;
                    claimIntimation.VehicleAvailablity = "";
                    claimIntimation.ClaimId = claimVM.ClaimId;
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.UserId = claimVM.UserId;
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                }
                else
                {
                    claimIntimation.WorkshopId = "0";
                    claimIntimation.WorkshopName = null;
                    claimIntimation.WorkshopType = null;
                    claimIntimation.VehicleAvailablity = "";
                    claimIntimation.ClaimId = claimVM.ClaimId;
                    claimIntimation.ClaimType_Id = Convert.ToInt32(claimVM.ClaimType);
                    claimIntimation.CurrentCity_Id = Convert.ToInt32(claimVM.CurrentCity);
                    claimIntimation.CurrentArea_Id = Convert.ToInt32(claimVM.CurrentArea);
                    claimIntimation.Area_Id = Convert.ToInt32(claimVM.IncidentArea);
                    claimIntimation.City_Id = Convert.ToInt32(claimVM.IncidentCity);
                    claimIntimation.SalesFormNo = claimVM.SalesFormNo;
                    claimIntimation.VehicleId = Convert.ToInt32(claimVM.Salesformdetailid);
                    claimIntimation.MobileNo = claimVM.MobileNo;
                    claimIntimation.Circumstance = claimVM.Circumstances;
                    claimIntimation.Email = claimVM.Email;
                    claimIntimation.PhoneNo = claimVM.PhoneNo;
                    claimIntimation.InsuredName = claimVM.InsuredName;
                    claimIntimation.Remarks = claimVM.Remarks;
                    claimIntimation.ThirdPartytag = false;
                    claimIntimation.ThirdPartyContact = null;
                    claimIntimation.ThirdPartyRemarks = null;
                    claimIntimation.RelationWithInsured = Convert.ToInt32(claimVM.RelationWithInsured);
                    claimIntimation.UserId = claimVM.UserId;
                    claimIntimation.IntimationQusAns = new List<IntimationQuestionAnswerVM>();
                    claimIntimation.Branch = !string.IsNullOrEmpty(claimVM.Branch) ? Convert.ToInt32(claimVM.Branch) : 0;
                    claimIntimation.ReserveAmount = claimVM.ReserveAmount;

                    if (claimVM.IncidentDate != null)
                    {
                        claimIntimation.IncidentDate = Convert.ToDateTime(claimVM.IncidentDate.Replace('-', ' '));
                    }

                    IntimationQuestionAnswerVM intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IsRequireTowTruck",
                        Answer = claimVM.IsRequireTowTruck
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IsRequireRentaCar",
                        Answer = claimVM.IsRequireRentaCar
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "IsThirdPartyAccident",
                        Answer = claimVM.IsThirdPartyAccident
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "VehicleDetailDD",
                        Answer = claimVM.VehicleDetailDD
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "RegistrationNo",
                        Answer = claimVM.RegisterationNo
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "VehicleMake",
                        Answer = claimVM.VehicleMake
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "VehicleModel",
                        Answer = claimVM.VehicleModel
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "EngineNo",
                        Answer = claimVM.EngineNo
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "ChassisNo",
                        Answer = claimVM.ChassisNo
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "NameInsuranceCompany",
                        Answer = claimVM.NameInsuranceCompany
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "PolicyPeriod",
                        Answer = claimVM.PolicyPeriod
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "PolicyReport",
                        Answer = claimVM.PolicyReport
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "ObjectType",
                        Answer = claimVM.ObjectType
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "ObjectDetail",
                        Answer = claimVM.ObjectDetail
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "ObjectLocation",
                        Answer = claimVM.ObjectDetail
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "DamageDetail1",
                        Answer = claimVM.DamageDetail1
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "DamageDetail2",
                        Answer = claimVM.DamageDetail2
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "DamageDetailRemarks",
                        Answer = claimVM.DamageDetailRemarks
                    };

                    claimIntimation.IntimationQusAns.Add(intimationQusAns);
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return claimIntimation;
        }

        private List<ClaimInfoVM> GetClaimInfo(int? salesformmappingId, int? salesFormDetailId, string headers)
        {
            List<ClaimInfoVM> claimInfoViewModelList = new List<ClaimInfoVM>();

            try
            {
                string url = $"{ConfigurationService.ClaimDetailByVehicle}{salesformmappingId}/{salesFormDetailId}";
                string response = HttpExtentions.GetRequest(url, headers);

                if (response == null)
                {
                    return claimInfoViewModelList;
                }

                claimInfoViewModelList = JsonConvert.DeserializeObject<List<ClaimInfoVM>>(response);

            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return claimInfoViewModelList;
        }

        public ResponseVM<List<PolicyInfoVM>> GetSearchDetail(List<SearchDetailVM> search, SearchVM searchVM, string headers)
        {
            ResponseVM<List<PolicyInfoVM>> response = new ResponseVM<List<PolicyInfoVM>>();

            try
            {
                List<SearchDetailVM> policylist = search.GroupBy(x => x.SalesFormNo).Select(x => x.First()).ToList();
                List<PolicyInfoVM> policyInfoViewModelsList = new List<PolicyInfoVM>();
                PolicyInfoVM policyobj = new PolicyInfoVM();

                for (int i = 0; i < policylist.Count; i++)
                {
                    policyobj = new PolicyInfoVM
                    {
                        InsuredName = policylist[i].InsuredName,
                        CustomerName = policylist[i].CustomerName,
                        SalesFormNo = policylist[i].SalesFormNo,
                        StartDate = policylist[i].StartDate,
                        ExpiryDate = policylist[i].ExpiryDate,
                        PolicyStatus = policylist[i].PolicyStatus,
                        Product = policylist[i].Product,
                        VAF = policylist[i].VAF,
                        Package_Id = policylist[i].Package_Id,
                        MobileNo = policylist[i].MobileNo,
                        SalesFormMapping_Id = policylist[i].SalesFormMapping_Id,
                        Salesformdetailid = policylist[i].SalesFormDetail_Id,
                        Contact_Id = policylist[i].Contact_Id,
                        Email = policylist[i].Email,
                        DeductibleAmount = policylist[i].DeductibleAmount,
                        Claimcount = policylist[i].Claimcount,
                        Outstanding = policylist[i].Outstanding,
                        VehicleList = new List<VehicleInfoVM>()
                    };

                    List<SearchDetailVM> vehiclelist = search.Where(x => x.SalesFormNo == search[i].SalesFormNo).ToList();

                    foreach (SearchDetailVM vehicle in vehiclelist)
                    {
                        List<ClaimInfoVM> claimList;

                        if (!string.IsNullOrEmpty(searchVM.ClaimNO))
                        {
                            claimList = GetClaimInfo(vehicle.SalesFormMapping_Id, vehicle.SalesFormDetail_Id, headers)
                                .Where(claim => claim.ClaimNo.Contains(searchVM.ClaimNO)).ToList();
                        }
                        else
                        {
                            claimList = GetClaimInfo(vehicle.SalesFormMapping_Id, vehicle.SalesFormDetail_Id, headers);
                        }

                        VehicleInfoVM vehicleobj = new VehicleInfoVM
                        {
                            RegistrationNo = vehicle.RegistrationNo,
                            Chasis_No = vehicle.Chasis_No,
                            Engine_No = vehicle.Engine_No,
                            StartDate = vehicle.StartDate,
                            ExpiryDate = vehicle.ExpiryDate,
                            VehicleStatus = vehicle.VehicleStatus,
                            SalesFormDetail_Id = vehicle.SalesFormDetail_Id,
                            Salesformmapping_Id = vehicle.SalesFormMapping_Id,
                            VehicleColor = vehicle.VehicleColor,
                            VehicleMake = vehicle.VehicleMake,
                            VehicleModel = vehicle.VehicleModel,
                            RegisterationYear = vehicle.RegisterationYear,
                            ClaimList = claimList
                        };

                        policyobj.VehicleList.Add(vehicleobj);
                    }

                    policyInfoViewModelsList.Add(policyobj);
                }

                double claimIntimationAmount = 0.0;
                double claimPaidAmount = 0.0;

                foreach (PolicyInfoVM policy in policyInfoViewModelsList)
                {
                    if (policy.PolicyStatus == "Active")
                    {
                        foreach (VehicleInfoVM vehicle in policy.VehicleList)
                        {
                            foreach (ClaimInfoVM claim in vehicle.ClaimList)
                            {
                                if (claim.ClaimIntimationAmount != null)
                                {
                                    claimIntimationAmount += Convert.ToDouble(claim.ClaimIntimationAmount);
                                }

                                if (claim.ClaimPaidAmount != null)
                                {
                                    claimPaidAmount += Convert.ToDouble(claim.ClaimPaidAmount);
                                }
                            }
                        }
                    }
                }

                return new ResponseVM<List<PolicyInfoVM>>
                {
                    ClaimIntimationAmount = claimIntimationAmount,
                    ClaimPaidAmount = claimPaidAmount,
                    Status = ResultStatus.Success,
                    Data = policyInfoViewModelsList,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);

                return new ResponseVM<List<PolicyInfoVM>>
                {
                    Data = null,
                    Status = ResultStatus.Error,
                    Message = ValidationMessages.NotFound
                };
            }
        }

        public List<SelectListItem> GetClaimType(int? claimId)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();

            try
            {
                string httpResponse = HttpExtentions.GetRequest($"{ConfigurationService.ClaimType}{claimId}");

                if (!string.IsNullOrEmpty(httpResponse))
                {
                    List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(httpResponse);

                    if (dropDownList != null)
                    {
                        selectListItemList = dropDownList.Select(x => new SelectListItem
                        {
                            Value = x.Id,
                            Text = x.Name
                        })
                     .Distinct()
                     .Where(x => x.Text.ToLower() != "death claim"
                                   && x.Text.ToLower() != "total-loss"
                                   && x.Text.ToLower() != "theft/snatch recovered"
                                   && !x.Text.ToLower().Contains("partial accident"))
                     .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemList;
        }

        public string GetClaimTypeByClaimId(int? claimId)
        {
            string productName = string.Empty;

            try
            {
                string response = HttpExtentions.GetRequest($"{ConfigurationService.ClaimTypeName}{claimId}");
                List<ProductTypeVM> productTypeList = JsonConvert.DeserializeObject<List<ProductTypeVM>>(response);
                productName = productTypeList.First().Product;
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return productName;
        }

        public List<SelectListItem> GetBranch()
        {
            List<SelectListItem> selectListItemsList = new List<SelectListItem>();

            try
            {
                string httpResponse = HttpExtentions.GetRequest(ConfigurationService.Branch);

                if (!string.IsNullOrEmpty(httpResponse))
                {
                    List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(httpResponse);

                    if (dropDownList != null)
                    {
                        selectListItemsList = dropDownList
                          .Select(x => new SelectListItem
                          {
                              Value = x.Id,
                              Text = x.Name
                          })
                          .Distinct()
                          .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemsList;
        }

        public List<SelectListItem> GetCity()
        {
            List<SelectListItem> selectListItemsList = new List<SelectListItem>();

            try
            {
                string response = HttpExtentions.GetRequest(ConfigurationService.City);
                if (response == null)
                {
                    return selectListItemsList;
                }

                List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(response);
                if (dropDownList == null)
                {
                    return selectListItemsList;
                }

                selectListItemsList = dropDownList.ToList().Select(x => new SelectListItem { Value = x.Id, Text = x.Name }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemsList;
        }

        public List<SelectListItem> GetAreaByCity(int? cityId)
        {
            List<SelectListItem> selectListItemsList = new List<SelectListItem>();

            try
            {
                string response = HttpExtentions.GetRequest($"{ConfigurationService.Area}{cityId}");
                if (response == null)
                {
                    return selectListItemsList;
                }

                List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(response);
                if (dropDownList == null)
                {
                    return selectListItemsList;
                }

                selectListItemsList = dropDownList.ToList().Select(x => new SelectListItem { Value = x.Id, Text = x.Name }).Distinct().ToList();

            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemsList;
        }

        public List<SelectListItem> GetRelation()
        {
            List<SelectListItem> selectListItemsList = new List<SelectListItem>();

            try
            {
                string response = HttpExtentions.GetRequest(ConfigurationService.Relation);
                if (response == null)
                {
                    return selectListItemsList;
                }

                List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(response);
                if (dropDownList == null)
                {
                    return selectListItemsList;
                }

                selectListItemsList = dropDownList.Select(x => new SelectListItem { Value = x.Id, Text = x.Name }).Distinct().ToList();

            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemsList;
        }

        public List<SelectListItem> GetDamageParts()
        {
            List<SelectListItem> selectListItemsList = new List<SelectListItem>();

            try
            {
                string response = HttpExtentions.GetRequest(ConfigurationService.DamageParts);
                if (response == null)
                {
                    return selectListItemsList;
                }

                List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(response);
                if (dropDownList == null)
                {
                    return selectListItemsList;
                }

                selectListItemsList = dropDownList.Select(x => new SelectListItem { Value = x.Id, Text = x.Name }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemsList;
        }

        public List<SelectListItem> GetWorkshopType(string workshopType)
        {
            List<SelectListItem> selectListItemsList = new List<SelectListItem>();

            try
            {
                string response = HttpExtentions.GetRequest($"{ConfigurationService.Workshop}{workshopType}");
                if (response == null)
                {
                    return selectListItemsList;
                }

                List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(response);
                if (dropDownList == null)
                {
                    return selectListItemsList;
                }

                selectListItemsList = dropDownList.Select(x => new SelectListItem { Value = x.Id, Text = x.Name }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemsList;
        }

        public List<SelectListItem> GetTrakkerCompany()
        {
            List<SelectListItem> selectListItemsList = new List<SelectListItem>();

            try
            {
                string response = HttpExtentions.GetRequest(ConfigurationService.TrakkerCompany);
                if (response == null)
                {
                    return selectListItemsList;
                }

                List<DropDownVM> dropDownList = JsonConvert.DeserializeObject<List<DropDownVM>>(response);
                if (dropDownList == null)
                {
                    return selectListItemsList;
                }

                selectListItemsList = dropDownList.Select(x => new SelectListItem { Value = x.Id, Text = x.Name }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return selectListItemsList;
        }

        public List<IntimationQuestionAnswerVM> GetClaimQs(string claimType, ClaimVM accident)
        {
            List<IntimationQuestionAnswerVM> objIntimationQs = new List<IntimationQuestionAnswerVM>();

            try
            {
                if (claimType.ToLower().Equals("Accident") || claimType.ToLower().Equals("Rain Water"))
                {
                    IntimationQuestionAnswerVM intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "Damageparts",
                        Answer = accident.Damageparts
                    };

                    objIntimationQs.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "WorkshopType",
                        Answer = accident.WorkshopType
                    };

                    objIntimationQs.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "Workshop",
                        Answer = accident.Workshop
                    };

                    objIntimationQs.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "WorkshopName",
                        Answer = accident.WorkshopName
                    };

                    objIntimationQs.Add(intimationQusAns);

                    intimationQusAns = new IntimationQuestionAnswerVM
                    {
                        Question_Id = "VehicleAvailablity",
                        Answer = accident.VehicleAvailablity
                    };

                    objIntimationQs.Add(intimationQusAns);
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return objIntimationQs;
        }

        public string GetDocumentList(string claimTypeText)
        {
            string response = string.Empty;

            try
            {
                response = HttpExtentions.GetRequest($"{ConfigurationService.ClaimDocumentList}{claimTypeText}");
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return response;
        }

        public string GetClaimReserve(string claimTypeText)
        {
            string response = string.Empty;

            try
            {
                response = HttpExtentions.GetRequest($"{ConfigurationService.ClaimReserve}{claimTypeText}");
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return response;
        }

        public List<ClaimGeneratedVM> SaveClaim(ClaimVM claimVM, string Authorization, string claimIntimationModuleName)
        {
            List<ClaimGeneratedVM> generatedClaimList = new List<ClaimGeneratedVM>
            {
                new ClaimGeneratedVM
                {
                    ClaimId = claimVM.ClaimId
                }
            };

            ClaimIntimationVM claimIntimationViewModel = new ClaimIntimationVM();

            Dictionary<string, ClaimIntimationVM> ClaimIntimationDictionary = new Dictionary<string, ClaimIntimationVM>
            {
                { "Accident", GetAccident(claimVM) },
                { "TheftSnatch", GetTheftSnatch(claimVM) },
                { "AccessoryTheft", GetAccessoriesTheft(claimVM) },
                { "ThirdParty", GetThirdPartyCoverage(claimVM) }
            };

            claimIntimationViewModel = ClaimIntimationDictionary[claimIntimationModuleName];

            string apiUrl = SetParameters(ref claimIntimationViewModel);

            if (!string.IsNullOrEmpty(claimVM.MobileNo) && !string.IsNullOrEmpty(claimVM.RelationWithInsured))
            {
                generatedClaimList = HttpExtentions.PostRequest<List<ClaimGeneratedVM>>(apiUrl, claimIntimationViewModel, Authorization);
            }

            if (claimVM.Remarks != null)
            {
                List<string> claimRemarksResponse = SaveClaimRemarks(claimVM.Remarks, claimVM.UserId, claimIntimationViewModel, generatedClaimList, Authorization);
            }

            return generatedClaimList;
        }

        public PolicyObjectVM GetPolicyObject(ClaimVM claimVM)
        {
            int? packageId = Convert.ToInt32(claimVM.Package_Id);

            PolicyObjectVM policyObject = new PolicyObjectVM
            {
                GetBranch = GetBranch(),
                GetClaimType = GetClaimType(packageId),
                GetCity = GetCity(),
                GetRelation = GetRelation(),
                GetDamageParts = GetDamageParts(),
                GetTrakkerCompany = GetTrakkerCompany(),
                SalesFormMapping_Id = claimVM.SalesFormMapping_Id,
                SalesFormDetail_Id = claimVM.Salesformdetailid,
                InsuredName = claimVM.InsuredName,
                ExpiryDate = claimVM.ExpiryDate,
                Contact_Id = claimVM.Contact_Id,
                MobileNo = claimVM.MobileNo,
                Email = claimVM.Email,
                Package_Id = claimVM.Package_Id,
                SalesFormNo = claimVM.SalesFormNo,
                DeductibleAmount = claimVM.DeductibleAmount,
                Outstanding = claimVM.Outstanding,
                Claimcount = claimVM.Claimcount,
                VehicleColor = claimVM.VehicleColor,
                EVAC = Convert.ToInt32(claimVM.EVAC),
                EngineNo = claimVM.EngineNo,
                ChassisNo = claimVM.ChassisNo,
                RegisterationNo = claimVM.RegisterationNo,
                VehicleMake = claimVM.VehicleMake,
                VehicleModel = claimVM.VehicleModel,
                YearOfManufacture = claimVM.YearOfManufacture,
                PolicyStartDate = claimVM.PolicyStartDate,
                PolicyEndDate = claimVM.PolicyEndDate,
                VehicleStartDate = claimVM.VehicleStartDate,
                VehicleEndDate = claimVM.VehicleEndDate,
                ClaimNo = claimVM.ClaimNo,
                ClaimId = claimVM.ClaimId,
            };

            if (claimVM.ClaimId != null)
            {
                policyObject.ClaimTypeName = GetClaimTypeByClaimId(Convert.ToInt32(claimVM.ClaimId));
            }

            return policyObject;
        }

        private List<IntimationQuestionAnswerVM> CheckClaimExists(int? claimId)
        {
            List<IntimationQuestionAnswerVM> intimationQusAnsList = new List<IntimationQuestionAnswerVM>();

            try
            {
                string result = HttpExtentions.GetRequest($"{ConfigurationService.CheckClaimExist}{claimId}");
                intimationQusAnsList = JsonConvert.DeserializeObject<List<IntimationQuestionAnswerVM>>(result);
            }
            catch (Exception ex)
            {
                LoggerService.LogExceptionsToDebugConsole(ex);
            }

            return intimationQusAnsList;
        }

        private readonly DateTime dateAdded = DateTime.Now;

        public string SetParameters(ref ClaimIntimationVM claimIntimationViewModel)
        {
            claimIntimationViewModel.Add_By = Convert.ToInt32(claimIntimationViewModel.UserId);
            claimIntimationViewModel.Add_IP = StringExtension.GetIPAddress();
            claimIntimationViewModel.Add_On = dateAdded;
            claimIntimationViewModel.Edit_By = Convert.ToInt32(claimIntimationViewModel.UserId);
            claimIntimationViewModel.Edit_IP = StringExtension.GetIPAddress();
            claimIntimationViewModel.Edit_On = dateAdded;

            string apiUrl = claimIntimationViewModel.ClaimId == null
                    ? ConfigurationService.ClaimIntimate
                    : ConfigurationService.UpdateClaim;

            return apiUrl;
        }

        public ClaimVM GetThirdPartyCoverage(ClaimIntimationVM claimIntimationViewModel)
        {
            ClaimVM thirdPartyCoverageViewModel = new ClaimVM
            {
                ClaimType = claimIntimationViewModel.ClaimType,
                IncidentDate = claimIntimationViewModel.IncidentDate.ToString("yyyy-MM-dd HH':'mm':'ss"),
                IncidentCity = claimIntimationViewModel.IncidentCity,
                IncidentArea = claimIntimationViewModel.IncidentArea,
                CurrentCity = claimIntimationViewModel.CurrentCity,
                CurrentArea = claimIntimationViewModel.CurrentArea,
                Circumstances = claimIntimationViewModel.Circumstances,
                InsuredName = claimIntimationViewModel.InsuredName,
                MobileNo = claimIntimationViewModel.Mobile,
                Email = claimIntimationViewModel.Email,
                PhoneNo = claimIntimationViewModel.PhoneNo,
                RelationWithInsured = claimIntimationViewModel.RelationWithInsured.ToString(),
                RemarksList = claimIntimationViewModel.RemarksList,
                VehicleDetailDD = claimIntimationViewModel.VehicleAvailablity,
                ClaimTypeName = claimIntimationViewModel.ClaimTypeName,
                Branch = Convert.ToString(claimIntimationViewModel.Branch_Id)
            };

            if (claimIntimationViewModel.IntimationQuestionAnswerList == null)
            {
                return thirdPartyCoverageViewModel;
            }

            foreach (IntimationQuestionAnswerVM item in claimIntimationViewModel.IntimationQuestionAnswerList)
            {
                switch (item.Question_Id)
                {
                    case "IsThirdPartyAccident":
                        thirdPartyCoverageViewModel.IsThirdPartyAccident = item.Answer;
                        break;
                    case "IsRequireTowTruck":
                        thirdPartyCoverageViewModel.IsRequireTowTruck = item.Answer;
                        break;
                    case "VehicleDetailDD":
                        thirdPartyCoverageViewModel.VehicleDetailDD = item.Answer;
                        break;
                    case "RegistrationNo":
                        thirdPartyCoverageViewModel.RegisterationNo = item.Answer;
                        break;
                    case "VehicleMake":
                        thirdPartyCoverageViewModel.VehicleMake = item.Answer;
                        break;
                    case "VehicleModel":
                        thirdPartyCoverageViewModel.VehicleModel = item.Answer;
                        break;
                    case "EngineNo":
                        thirdPartyCoverageViewModel.EngineNo = item.Answer;
                        break;
                    case "ChassisNo":
                        thirdPartyCoverageViewModel.ChassisNo = item.Answer;
                        break;
                    case "InsuredName":
                        thirdPartyCoverageViewModel.InsuredName = item.Answer;
                        break;
                    case "PolicyPeriod":
                        thirdPartyCoverageViewModel.PolicyPeriod = item.Answer;
                        break;
                    case "PolicyReport":
                        thirdPartyCoverageViewModel.PolicyReport = item.Answer;
                        break;
                    case "ObjectType":
                        thirdPartyCoverageViewModel.ObjectType = item.Answer;
                        break;
                    case "ObjectDetail":
                        thirdPartyCoverageViewModel.ObjectDetail = item.Answer;
                        break;
                    case "ObjectLocation":
                        thirdPartyCoverageViewModel.ObjectLocation = item.Answer;
                        break;
                    case "DamageDetail1":
                        thirdPartyCoverageViewModel.DamageDetail1 = item.Answer;
                        break;
                    case "DamageDetail2":
                        thirdPartyCoverageViewModel.DamageDetail2 = item.Answer;
                        break;
                    case "DamageDetailRemarks":
                        thirdPartyCoverageViewModel.DamageDetailRemarks = item.Answer;
                        break;
                    case "NameInsuranceCompany":
                        thirdPartyCoverageViewModel.NameInsuranceCompany = item.Answer;
                        break;
                }
            }

            return thirdPartyCoverageViewModel;
        }

        public ClaimVM GetAccidentRainCoverage(ClaimIntimationVM claimIntimationViewModel)
        {
            ClaimVM accidentViewModel = new ClaimVM
            {
                ClaimType = claimIntimationViewModel.ClaimType,
                IncidentDate = claimIntimationViewModel.IncidentDate.ToString("yyyy-MM-dd HH':'mm':'ss"),
                IncidentCity = claimIntimationViewModel.IncidentCity,
                IncidentArea = claimIntimationViewModel.IncidentArea,
                CurrentCity = claimIntimationViewModel.CurrentCity,
                CurrentArea = claimIntimationViewModel.CurrentArea,
                Circumstances = claimIntimationViewModel.Circumstances,
                InsuredName = claimIntimationViewModel.InsuredName,
                MobileNo = claimIntimationViewModel.Mobile,
                Email = claimIntimationViewModel.Email,
                PhoneNo = claimIntimationViewModel.PhoneNo,
                RelationWithInsured = claimIntimationViewModel.RelationWithInsured.ToString(),
                RemarksList = claimIntimationViewModel.RemarksList,
                ClaimTypeName = claimIntimationViewModel.ClaimTypeName,
                Damageparts = claimIntimationViewModel.NoOfDamagedParts,
                Workshop = claimIntimationViewModel.WorkshopId,
                WorkshopType = claimIntimationViewModel.WorkshopType,
                VehicleAvailablity = claimIntimationViewModel.VehicleAvailablity,
                WorkshopCollection = claimIntimationViewModel.WorkshopCollection,
                Branch = Convert.ToString(claimIntimationViewModel.Branch_Id)
            };

            if (claimIntimationViewModel.IntimationQuestionAnswerList == null)
            {
                return accidentViewModel;
            }

            GetHeavyDamagePartsFromAnswers(claimIntimationViewModel, accidentViewModel);

            if (accidentViewModel.WorkshopCollection != null)
            {
                accidentViewModel.WorkshopCollection =
                    accidentViewModel.WorkshopCollection
                    .GroupBy(i => i.WorkshopId)
                    .Select(g => g.First())
                    .ToList();
            }

            AddAnswerWorkshopToWorkshopCollection(claimIntimationViewModel, accidentViewModel);

            return accidentViewModel;
        }

        public ClaimVM GetAccessoryTheft(ClaimIntimationVM claimIntimationViewModel)
        {
            ClaimVM accessoryTheftViewModel = new ClaimVM
            {
                ClaimType = claimIntimationViewModel.ClaimType,
                IncidentDate = claimIntimationViewModel.IncidentDate.ToString("yyyy-MM-dd HH':'mm':'ss"),
                IncidentCity = claimIntimationViewModel.IncidentCity,
                IncidentArea = claimIntimationViewModel.IncidentArea,
                CurrentCity = claimIntimationViewModel.CurrentCity,
                CurrentArea = claimIntimationViewModel.CurrentArea,
                Circumstances = claimIntimationViewModel.Circumstances,
                InsuredName = claimIntimationViewModel.InsuredName,
                MobileNo = claimIntimationViewModel.Mobile,
                Email = claimIntimationViewModel.Email,
                PhoneNo = claimIntimationViewModel.PhoneNo,
                RelationWithInsured = claimIntimationViewModel.RelationWithInsured.ToString(),
                RemarksList = claimIntimationViewModel.RemarksList,
                Damageparts = claimIntimationViewModel.NoOfDamagedParts,
                ClaimTypeName = claimIntimationViewModel.ClaimTypeName,
                Workshop = claimIntimationViewModel.WorkshopId,
                WorkshopType = claimIntimationViewModel.WorkshopType,
                VehicleAvailablity = claimIntimationViewModel.VehicleAvailablity,
                WorkshopCollection = claimIntimationViewModel.WorkshopCollection,
                Branch = Convert.ToString(claimIntimationViewModel.Branch_Id)
            };

            if (claimIntimationViewModel.IntimationQuestionAnswerList == null)
            {
                return accessoryTheftViewModel;
            }

            GetHeavyDamagePartsFromAnswers(claimIntimationViewModel, accessoryTheftViewModel);

            if (accessoryTheftViewModel.WorkshopCollection != null)
            {
                accessoryTheftViewModel.WorkshopCollection = accessoryTheftViewModel.WorkshopCollection.GroupBy(i => i.WorkshopId).Select(g => g.First()).ToList();
            }

            AddAnswerWorkshopToWorkshopCollection(claimIntimationViewModel, accessoryTheftViewModel);

            return accessoryTheftViewModel;
        }

        public void GetHeavyDamagePartsFromAnswers(ClaimIntimationVM claimIntimationViewModel, ClaimVM claimVM)
        {
            foreach (IntimationQuestionAnswerVM item in claimIntimationViewModel.IntimationQuestionAnswerList)
            {
                switch (item.Question_Id)
                {
                    case "HeavyDamagePart":
                        claimVM.HeavyDamagePart = item.Answer;
                        break;
                    case "HeavyDamagePartRemarks":
                        claimVM.HeavyDamagePartRemarks = item.Answer;
                        break;
                }
            }
        }

        public void AddAnswerWorkshopToWorkshopCollection(ClaimIntimationVM claimIntimationViewModel, ClaimVM claimVM)
        {
            List<WorkshopVM> workshop = GetWorkshopFromAnswers(claimIntimationViewModel);

            for (int w = 0; w < workshop.Count; w++)
            {
                if (claimVM.WorkshopCollection != null &&
                    claimVM.WorkshopCollection.All(x => x.WorkshopId != workshop[w].WorkshopId) &&
                    !string.IsNullOrEmpty(workshop[w].WorkshopId))
                {
                    claimVM.WorkshopCollection.Add(workshop[w]);
                }
            }
        }

        public List<WorkshopVM> GetWorkshopFromAnswers(ClaimIntimationVM claimIntimationViewModel)
        {
            WorkshopVM workshop = new WorkshopVM();
            List<WorkshopVM> wkList = new List<WorkshopVM>();
            List<IntimationQuestionAnswerVM> wList = claimIntimationViewModel.IntimationQuestionAnswerList
                .Where(x => x.Question_Id.Contains("Workshop") ||
                x.Question_Id.Contains("Wokshop") ||
                x.Question_Id.Contains("VehicleAvailablity"))
                .ToList();
            int wkCount = 0;

            foreach (IntimationQuestionAnswerVM item in wList)
            {
                switch (item.Question_Id)
                {
                    case "Workshop":
                        workshop.WorkshopId = item.Answer;
                        wkCount++;
                        break;
                    case "WokshopName":
                        workshop.WorkshopName = item.Answer;
                        wkCount++;
                        break;
                    case "WorkshopType":
                        workshop.WorkshopType = item.Answer;
                        wkCount++;
                        break;
                    case "VehicleAvailablity":
                        workshop.VehicleAvailablity = item.Answer;
                        wkCount++;
                        break;
                }

                if (wkCount % 4 == 0)
                {
                    wkList.Add(workshop);
                    workshop = new WorkshopVM();
                }
            }

            return wkList;
        }

        public ClaimVM GetTheftSnatch(ClaimIntimationVM claimIntimationViewModel)
        {
            ClaimVM theftSnatchViewModel = new ClaimVM
            {
                ClaimType = claimIntimationViewModel.ClaimType,
                IncidentDate = claimIntimationViewModel.IncidentDate.ToString("yyyy-MM-dd HH':'mm':'ss"),
                IncidentCity = claimIntimationViewModel.IncidentCity,
                IncidentArea = claimIntimationViewModel.IncidentArea,
                CurrentCity = claimIntimationViewModel.CurrentCity,
                CurrentArea = claimIntimationViewModel.CurrentArea,
                Circumstances = claimIntimationViewModel.Circumstances,
                InsuredName = claimIntimationViewModel.InsuredName,
                MobileNo = claimIntimationViewModel.Mobile,
                Email = claimIntimationViewModel.Email,
                PhoneNo = claimIntimationViewModel.PhoneNo,
                RelationWithInsured = claimIntimationViewModel.RelationWithInsured.ToString(),
                RemarksList = claimIntimationViewModel.RemarksList,
                ClaimTypeName = claimIntimationViewModel.ClaimTypeName,
                Damageparts = claimIntimationViewModel.NoOfDamagedParts,
                VehicleAvailablity = claimIntimationViewModel.VehicleAvailablity,
                Branch = Convert.ToString(claimIntimationViewModel.Branch_Id)
            };

            if (claimIntimationViewModel.IntimationQuestionAnswerList == null)
            {
                return theftSnatchViewModel;
            }

            foreach (IntimationQuestionAnswerVM item in claimIntimationViewModel.IntimationQuestionAnswerList)
            {
                switch (item.Question_Id)
                {
                    case "IntimateService":
                        theftSnatchViewModel.IntimateService = item.Answer;
                        break;
                    case "ComplaintNo":
                        theftSnatchViewModel.ComplaintNo = item.Answer;
                        break;
                    case "TrakkerInstall":
                        theftSnatchViewModel.TrakkerInstall = item.Answer;
                        break;
                    case "TrackerCompanyName":
                        theftSnatchViewModel.TrackerCompanyName = item.Answer;
                        break;
                    case "HeavyDamagePart":
                        theftSnatchViewModel.HeavyDamagePart = item.Answer;
                        break;
                    case "HeavyDamagePartRemarks":
                        theftSnatchViewModel.HeavyDamagePartRemarks = item.Answer;
                        break;
                }
            }

            return theftSnatchViewModel;
        }

        public List<string> SaveClaimRemarks(List<string> remarksList, string userId, ClaimIntimationVM claimIntimationViewModel, List<ClaimGeneratedVM> generatedClaimList, string headers)
        {
            List<ClaimRemarksVM> claimRemarksList = new List<ClaimRemarksVM>();
            ClaimRemarksVM claimRemarks = null;
            
                foreach (string remark in remarksList)
                {
                    claimRemarks = new ClaimRemarksVM
                    {
                        Add_Ip = StringExtension.GetIPAddress(),
                        Remarks = remark,
                        Add_By = userId,
                        ClaimForm_Id = claimIntimationViewModel.ClaimId ?? generatedClaimList[0].ClaimId,
                        Add_On = DateTime.Now
                    };
                }
                claimRemarksList.Add(claimRemarks);
            

            List<string> response = HttpExtentions.PostRequest<List<string>>(ConfigurationService.SaveClaimRemarks, claimRemarksList, headers);

            return response;
        }

        public List<WorkshopVM> LoadWorkshopDetail(string authorization, int? claimId, ClaimIntimationVM claimIntimationViewModel)
        {
            string workshopResult = HttpExtentions.GetRequest($"{ConfigurationService.WorkshopDetail}{claimId}", authorization);
            List<WorkshopVM> workshopList = JsonConvert.DeserializeObject<List<WorkshopVM>>(workshopResult);
            var answersList = new List<WorkshopVM>();

            if (claimIntimationViewModel.getAnswer != null && claimIntimationViewModel.getAnswer.Count > 0)
            {
                var workshop = new WorkshopVM();
                int wkCount = 0;

                foreach (var item in claimIntimationViewModel.getAnswer)
                {
                    if (item.question_Id == "Workshop")
                    {
                        workshop.WorkshopId = item.answer;
                        wkCount++;
                    }
                    else if (item.question_Id == "WokshopName")
                    {
                        workshop.WorkshopName = item.answer;
                        wkCount++;
                    }
                    else if (item.question_Id == "WorkshopType")
                    {
                        workshop.WorkshopType = item.answer;
                        wkCount++;
                    }
                    else if (item.question_Id == "VehicleAvailablity")
                    {
                        workshop.VehicleAvailablity = item.answer;
                        workshop.AppointmentDate = StringExtension.ToDateTimeString(StringExtension.ToDateString(item.answer.ToString(), true));
                        wkCount++;
                    }

                    if (wkCount % 4 == 0)
                    {
                        if (workshopList.All(x => x.WorkshopId != workshop.WorkshopId) && !string.IsNullOrEmpty(workshop.WorkshopId))
                        {
                            workshopList.Add(workshop);
                            workshop = new WorkshopVM();
                        }
                    }
                }
            }

            return workshopList;
        }
    }
}