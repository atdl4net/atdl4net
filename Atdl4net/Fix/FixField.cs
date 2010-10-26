#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
//
//   This software is released under both commercial and open-source licenses.
//
//   If you received this software under the commercial license, the terms of that license can be found in the
//   Commercial.txt file in the Licenses folder.  If you received this software under the open-source license,
//   the following applies:
//
//      This file is part of Atdl4net.
//
//      Atdl4net is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public 
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion


// NOTE: This code is auto-generated; editing by hand is not recommended.

namespace Atdl4net.Fix
{
	public enum FixField
	{
		/// <summary>FIX Account tag (tag 1).</summary>
		FIX_Account = 1,

		/// <summary>FIX AdvId tag (tag 2).</summary>
		FIX_AdvId = 2,

		/// <summary>FIX AdvRefID tag (tag 3).</summary>
		FIX_AdvRefID = 3,

		/// <summary>FIX AdvSide tag (tag 4).</summary>
		FIX_AdvSide = 4,

		/// <summary>FIX AdvTransType tag (tag 5).</summary>
		FIX_AdvTransType = 5,

		/// <summary>FIX AvgPx tag (tag 6).</summary>
		FIX_AvgPx = 6,

		/// <summary>FIX BeginSeqNo tag (tag 7).</summary>
		FIX_BeginSeqNo = 7,

		/// <summary>FIX BeginString tag (tag 8).</summary>
		FIX_BeginString = 8,

		/// <summary>FIX BodyLength tag (tag 9).</summary>
		FIX_BodyLength = 9,

		/// <summary>FIX CheckSum tag (tag 10).</summary>
		FIX_CheckSum = 10,

		/// <summary>FIX ClOrdID tag (tag 11).</summary>
		FIX_ClOrdID = 11,

		/// <summary>FIX Commission tag (tag 12).</summary>
		FIX_Commission = 12,

		/// <summary>FIX CommType tag (tag 13).</summary>
		FIX_CommType = 13,

		/// <summary>FIX CumQty tag (tag 14).</summary>
		FIX_CumQty = 14,

		/// <summary>FIX Currency tag (tag 15).</summary>
		FIX_Currency = 15,

		/// <summary>FIX EndSeqNo tag (tag 16).</summary>
		FIX_EndSeqNo = 16,

		/// <summary>FIX ExecID tag (tag 17).</summary>
		FIX_ExecID = 17,

		/// <summary>FIX ExecInst tag (tag 18).</summary>
		FIX_ExecInst = 18,

		/// <summary>FIX ExecRefID tag (tag 19).</summary>
		FIX_ExecRefID = 19,

		/// <summary>FIX HandlInst tag (tag 21).</summary>
		FIX_HandlInst = 21,

		/// <summary>FIX SecurityIDSource tag (tag 22).</summary>
		FIX_SecurityIDSource = 22,

		/// <summary>FIX IOIID tag (tag 23).</summary>
		FIX_IOIID = 23,

		/// <summary>FIX IOIQltyInd tag (tag 25).</summary>
		FIX_IOIQltyInd = 25,

		/// <summary>FIX IOIRefID tag (tag 26).</summary>
		FIX_IOIRefID = 26,

		/// <summary>FIX IOIQty tag (tag 27).</summary>
		FIX_IOIQty = 27,

		/// <summary>FIX IOITransType tag (tag 28).</summary>
		FIX_IOITransType = 28,

		/// <summary>FIX LastCapacity tag (tag 29).</summary>
		FIX_LastCapacity = 29,

		/// <summary>FIX LastMkt tag (tag 30).</summary>
		FIX_LastMkt = 30,

		/// <summary>FIX LastPx tag (tag 31).</summary>
		FIX_LastPx = 31,

		/// <summary>FIX LastQty tag (tag 32).</summary>
		FIX_LastQty = 32,

		/// <summary>FIX NoLinesOfText tag (tag 33).</summary>
		FIX_NoLinesOfText = 33,

		/// <summary>FIX MsgSeqNum tag (tag 34).</summary>
		FIX_MsgSeqNum = 34,

		/// <summary>FIX MsgType tag (tag 35).</summary>
		FIX_MsgType = 35,

		/// <summary>FIX NewSeqNo tag (tag 36).</summary>
		FIX_NewSeqNo = 36,

		/// <summary>FIX OrderID tag (tag 37).</summary>
		FIX_OrderID = 37,

		/// <summary>FIX OrderQty tag (tag 38).</summary>
		FIX_OrderQty = 38,

		/// <summary>FIX OrdStatus tag (tag 39).</summary>
		FIX_OrdStatus = 39,

		/// <summary>FIX OrdType tag (tag 40).</summary>
		FIX_OrdType = 40,

		/// <summary>FIX OrigClOrdID tag (tag 41).</summary>
		FIX_OrigClOrdID = 41,

		/// <summary>FIX OrigTime tag (tag 42).</summary>
		FIX_OrigTime = 42,

		/// <summary>FIX PossDupFlag tag (tag 43).</summary>
		FIX_PossDupFlag = 43,

		/// <summary>FIX Price tag (tag 44).</summary>
		FIX_Price = 44,

		/// <summary>FIX RefSeqNum tag (tag 45).</summary>
		FIX_RefSeqNum = 45,

		/// <summary>FIX SecurityID tag (tag 48).</summary>
		FIX_SecurityID = 48,

		/// <summary>FIX SenderCompID tag (tag 49).</summary>
		FIX_SenderCompID = 49,

		/// <summary>FIX SenderSubID tag (tag 50).</summary>
		FIX_SenderSubID = 50,

		/// <summary>FIX SendingTime tag (tag 52).</summary>
		FIX_SendingTime = 52,

		/// <summary>FIX Quantity tag (tag 53).</summary>
		FIX_Quantity = 53,

		/// <summary>FIX Side tag (tag 54).</summary>
		FIX_Side = 54,

		/// <summary>FIX Symbol tag (tag 55).</summary>
		FIX_Symbol = 55,

		/// <summary>FIX TargetCompID tag (tag 56).</summary>
		FIX_TargetCompID = 56,

		/// <summary>FIX TargetSubID tag (tag 57).</summary>
		FIX_TargetSubID = 57,

		/// <summary>FIX Text tag (tag 58).</summary>
		FIX_Text = 58,

		/// <summary>FIX TimeInForce tag (tag 59).</summary>
		FIX_TimeInForce = 59,

		/// <summary>FIX TransactTime tag (tag 60).</summary>
		FIX_TransactTime = 60,

		/// <summary>FIX Urgency tag (tag 61).</summary>
		FIX_Urgency = 61,

		/// <summary>FIX ValidUntilTime tag (tag 62).</summary>
		FIX_ValidUntilTime = 62,

		/// <summary>FIX SettlType tag (tag 63).</summary>
		FIX_SettlType = 63,

		/// <summary>FIX SettlDate tag (tag 64).</summary>
		FIX_SettlDate = 64,

		/// <summary>FIX SymbolSfx tag (tag 65).</summary>
		FIX_SymbolSfx = 65,

		/// <summary>FIX ListID tag (tag 66).</summary>
		FIX_ListID = 66,

		/// <summary>FIX ListSeqNo tag (tag 67).</summary>
		FIX_ListSeqNo = 67,

		/// <summary>FIX TotNoOrders tag (tag 68).</summary>
		FIX_TotNoOrders = 68,

		/// <summary>FIX ListExecInst tag (tag 69).</summary>
		FIX_ListExecInst = 69,

		/// <summary>FIX AllocID tag (tag 70).</summary>
		FIX_AllocID = 70,

		/// <summary>FIX AllocTransType tag (tag 71).</summary>
		FIX_AllocTransType = 71,

		/// <summary>FIX RefAllocID tag (tag 72).</summary>
		FIX_RefAllocID = 72,

		/// <summary>FIX NoOrders tag (tag 73).</summary>
		FIX_NoOrders = 73,

		/// <summary>FIX AvgPxPrecision tag (tag 74).</summary>
		FIX_AvgPxPrecision = 74,

		/// <summary>FIX TradeDate tag (tag 75).</summary>
		FIX_TradeDate = 75,

		/// <summary>FIX PositionEffect tag (tag 77).</summary>
		FIX_PositionEffect = 77,

		/// <summary>FIX NoAllocs tag (tag 78).</summary>
		FIX_NoAllocs = 78,

		/// <summary>FIX AllocAccount tag (tag 79).</summary>
		FIX_AllocAccount = 79,

		/// <summary>FIX AllocQty tag (tag 80).</summary>
		FIX_AllocQty = 80,

		/// <summary>FIX ProcessCode tag (tag 81).</summary>
		FIX_ProcessCode = 81,

		/// <summary>FIX NoRpts tag (tag 82).</summary>
		FIX_NoRpts = 82,

		/// <summary>FIX RptSeq tag (tag 83).</summary>
		FIX_RptSeq = 83,

		/// <summary>FIX CxlQty tag (tag 84).</summary>
		FIX_CxlQty = 84,

		/// <summary>FIX NoDlvyInst tag (tag 85).</summary>
		FIX_NoDlvyInst = 85,

		/// <summary>FIX AllocStatus tag (tag 87).</summary>
		FIX_AllocStatus = 87,

		/// <summary>FIX AllocRejCode tag (tag 88).</summary>
		FIX_AllocRejCode = 88,

		/// <summary>FIX Signature tag (tag 89).</summary>
		FIX_Signature = 89,

		/// <summary>FIX SecureDataLen tag (tag 90).</summary>
		FIX_SecureDataLen = 90,

		/// <summary>FIX SecureData tag (tag 91).</summary>
		FIX_SecureData = 91,

		/// <summary>FIX SignatureLength tag (tag 93).</summary>
		FIX_SignatureLength = 93,

		/// <summary>FIX EmailType tag (tag 94).</summary>
		FIX_EmailType = 94,

		/// <summary>FIX RawDataLength tag (tag 95).</summary>
		FIX_RawDataLength = 95,

		/// <summary>FIX RawData tag (tag 96).</summary>
		FIX_RawData = 96,

		/// <summary>FIX PossResend tag (tag 97).</summary>
		FIX_PossResend = 97,

		/// <summary>FIX EncryptMethod tag (tag 98).</summary>
		FIX_EncryptMethod = 98,

		/// <summary>FIX StopPx tag (tag 99).</summary>
		FIX_StopPx = 99,

		/// <summary>FIX ExDestination tag (tag 100).</summary>
		FIX_ExDestination = 100,

		/// <summary>FIX CxlRejReason tag (tag 102).</summary>
		FIX_CxlRejReason = 102,

		/// <summary>FIX OrdRejReason tag (tag 103).</summary>
		FIX_OrdRejReason = 103,

		/// <summary>FIX IOIQualifier tag (tag 104).</summary>
		FIX_IOIQualifier = 104,

		/// <summary>FIX Issuer tag (tag 106).</summary>
		FIX_Issuer = 106,

		/// <summary>FIX SecurityDesc tag (tag 107).</summary>
		FIX_SecurityDesc = 107,

		/// <summary>FIX HeartBtInt tag (tag 108).</summary>
		FIX_HeartBtInt = 108,

		/// <summary>FIX MinQty tag (tag 110).</summary>
		FIX_MinQty = 110,

		/// <summary>FIX MaxFloor tag (tag 111).</summary>
		FIX_MaxFloor = 111,

		/// <summary>FIX TestReqID tag (tag 112).</summary>
		FIX_TestReqID = 112,

		/// <summary>FIX ReportToExch tag (tag 113).</summary>
		FIX_ReportToExch = 113,

		/// <summary>FIX LocateReqd tag (tag 114).</summary>
		FIX_LocateReqd = 114,

		/// <summary>FIX OnBehalfOfCompID tag (tag 115).</summary>
		FIX_OnBehalfOfCompID = 115,

		/// <summary>FIX OnBehalfOfSubID tag (tag 116).</summary>
		FIX_OnBehalfOfSubID = 116,

		/// <summary>FIX QuoteID tag (tag 117).</summary>
		FIX_QuoteID = 117,

		/// <summary>FIX NetMoney tag (tag 118).</summary>
		FIX_NetMoney = 118,

		/// <summary>FIX SettlCurrAmt tag (tag 119).</summary>
		FIX_SettlCurrAmt = 119,

		/// <summary>FIX SettlCurrency tag (tag 120).</summary>
		FIX_SettlCurrency = 120,

		/// <summary>FIX ForexReq tag (tag 121).</summary>
		FIX_ForexReq = 121,

		/// <summary>FIX OrigSendingTime tag (tag 122).</summary>
		FIX_OrigSendingTime = 122,

		/// <summary>FIX GapFillFlag tag (tag 123).</summary>
		FIX_GapFillFlag = 123,

		/// <summary>FIX NoExecs tag (tag 124).</summary>
		FIX_NoExecs = 124,

		/// <summary>FIX ExpireTime tag (tag 126).</summary>
		FIX_ExpireTime = 126,

		/// <summary>FIX DKReason tag (tag 127).</summary>
		FIX_DKReason = 127,

		/// <summary>FIX DeliverToCompID tag (tag 128).</summary>
		FIX_DeliverToCompID = 128,

		/// <summary>FIX DeliverToSubID tag (tag 129).</summary>
		FIX_DeliverToSubID = 129,

		/// <summary>FIX IOINaturalFlag tag (tag 130).</summary>
		FIX_IOINaturalFlag = 130,

		/// <summary>FIX QuoteReqID tag (tag 131).</summary>
		FIX_QuoteReqID = 131,

		/// <summary>FIX BidPx tag (tag 132).</summary>
		FIX_BidPx = 132,

		/// <summary>FIX OfferPx tag (tag 133).</summary>
		FIX_OfferPx = 133,

		/// <summary>FIX BidSize tag (tag 134).</summary>
		FIX_BidSize = 134,

		/// <summary>FIX OfferSize tag (tag 135).</summary>
		FIX_OfferSize = 135,

		/// <summary>FIX NoMiscFees tag (tag 136).</summary>
		FIX_NoMiscFees = 136,

		/// <summary>FIX MiscFeeAmt tag (tag 137).</summary>
		FIX_MiscFeeAmt = 137,

		/// <summary>FIX MiscFeeCurr tag (tag 138).</summary>
		FIX_MiscFeeCurr = 138,

		/// <summary>FIX MiscFeeType tag (tag 139).</summary>
		FIX_MiscFeeType = 139,

		/// <summary>FIX PrevClosePx tag (tag 140).</summary>
		FIX_PrevClosePx = 140,

		/// <summary>FIX ResetSeqNumFlag tag (tag 141).</summary>
		FIX_ResetSeqNumFlag = 141,

		/// <summary>FIX SenderLocationID tag (tag 142).</summary>
		FIX_SenderLocationID = 142,

		/// <summary>FIX TargetLocationID tag (tag 143).</summary>
		FIX_TargetLocationID = 143,

		/// <summary>FIX OnBehalfOfLocationID tag (tag 144).</summary>
		FIX_OnBehalfOfLocationID = 144,

		/// <summary>FIX DeliverToLocationID tag (tag 145).</summary>
		FIX_DeliverToLocationID = 145,

		/// <summary>FIX NoRelatedSym tag (tag 146).</summary>
		FIX_NoRelatedSym = 146,

		/// <summary>FIX Subject tag (tag 147).</summary>
		FIX_Subject = 147,

		/// <summary>FIX Headline tag (tag 148).</summary>
		FIX_Headline = 148,

		/// <summary>FIX URLLink tag (tag 149).</summary>
		FIX_URLLink = 149,

		/// <summary>FIX ExecType tag (tag 150).</summary>
		FIX_ExecType = 150,

		/// <summary>FIX LeavesQty tag (tag 151).</summary>
		FIX_LeavesQty = 151,

		/// <summary>FIX CashOrderQty tag (tag 152).</summary>
		FIX_CashOrderQty = 152,

		/// <summary>FIX AllocAvgPx tag (tag 153).</summary>
		FIX_AllocAvgPx = 153,

		/// <summary>FIX AllocNetMoney tag (tag 154).</summary>
		FIX_AllocNetMoney = 154,

		/// <summary>FIX SettlCurrFxRate tag (tag 155).</summary>
		FIX_SettlCurrFxRate = 155,

		/// <summary>FIX SettlCurrFxRateCalc tag (tag 156).</summary>
		FIX_SettlCurrFxRateCalc = 156,

		/// <summary>FIX NumDaysInterest tag (tag 157).</summary>
		FIX_NumDaysInterest = 157,

		/// <summary>FIX AccruedInterestRate tag (tag 158).</summary>
		FIX_AccruedInterestRate = 158,

		/// <summary>FIX AccruedInterestAmt tag (tag 159).</summary>
		FIX_AccruedInterestAmt = 159,

		/// <summary>FIX SettlInstMode tag (tag 160).</summary>
		FIX_SettlInstMode = 160,

		/// <summary>FIX AllocText tag (tag 161).</summary>
		FIX_AllocText = 161,

		/// <summary>FIX SettlInstID tag (tag 162).</summary>
		FIX_SettlInstID = 162,

		/// <summary>FIX SettlInstTransType tag (tag 163).</summary>
		FIX_SettlInstTransType = 163,

		/// <summary>FIX EmailThreadID tag (tag 164).</summary>
		FIX_EmailThreadID = 164,

		/// <summary>FIX SettlInstSource tag (tag 165).</summary>
		FIX_SettlInstSource = 165,

		/// <summary>FIX SecurityType tag (tag 167).</summary>
		FIX_SecurityType = 167,

		/// <summary>FIX EffectiveTime tag (tag 168).</summary>
		FIX_EffectiveTime = 168,

		/// <summary>FIX StandInstDbType tag (tag 169).</summary>
		FIX_StandInstDbType = 169,

		/// <summary>FIX StandInstDbName tag (tag 170).</summary>
		FIX_StandInstDbName = 170,

		/// <summary>FIX StandInstDbID tag (tag 171).</summary>
		FIX_StandInstDbID = 171,

		/// <summary>FIX SettlDeliveryType tag (tag 172).</summary>
		FIX_SettlDeliveryType = 172,

		/// <summary>FIX BidSpotRate tag (tag 188).</summary>
		FIX_BidSpotRate = 188,

		/// <summary>FIX BidForwardPoints tag (tag 189).</summary>
		FIX_BidForwardPoints = 189,

		/// <summary>FIX OfferSpotRate tag (tag 190).</summary>
		FIX_OfferSpotRate = 190,

		/// <summary>FIX OfferForwardPoints tag (tag 191).</summary>
		FIX_OfferForwardPoints = 191,

		/// <summary>FIX OrderQty2 tag (tag 192).</summary>
		FIX_OrderQty2 = 192,

		/// <summary>FIX SettlDate2 tag (tag 193).</summary>
		FIX_SettlDate2 = 193,

		/// <summary>FIX LastSpotRate tag (tag 194).</summary>
		FIX_LastSpotRate = 194,

		/// <summary>FIX LastForwardPoints tag (tag 195).</summary>
		FIX_LastForwardPoints = 195,

		/// <summary>FIX AllocLinkID tag (tag 196).</summary>
		FIX_AllocLinkID = 196,

		/// <summary>FIX AllocLinkType tag (tag 197).</summary>
		FIX_AllocLinkType = 197,

		/// <summary>FIX SecondaryOrderID tag (tag 198).</summary>
		FIX_SecondaryOrderID = 198,

		/// <summary>FIX NoIOIQualifiers tag (tag 199).</summary>
		FIX_NoIOIQualifiers = 199,

		/// <summary>FIX MaturityMonthYear tag (tag 200).</summary>
		FIX_MaturityMonthYear = 200,

		/// <summary>FIX PutOrCall tag (tag 201).</summary>
		FIX_PutOrCall = 201,

		/// <summary>FIX StrikePrice tag (tag 202).</summary>
		FIX_StrikePrice = 202,

		/// <summary>FIX CoveredOrUncovered tag (tag 203).</summary>
		FIX_CoveredOrUncovered = 203,

		/// <summary>FIX OptAttribute tag (tag 206).</summary>
		FIX_OptAttribute = 206,

		/// <summary>FIX SecurityExchange tag (tag 207).</summary>
		FIX_SecurityExchange = 207,

		/// <summary>FIX NotifyBrokerOfCredit tag (tag 208).</summary>
		FIX_NotifyBrokerOfCredit = 208,

		/// <summary>FIX AllocHandlInst tag (tag 209).</summary>
		FIX_AllocHandlInst = 209,

		/// <summary>FIX MaxShow tag (tag 210).</summary>
		FIX_MaxShow = 210,

		/// <summary>FIX PegOffsetValue tag (tag 211).</summary>
		FIX_PegOffsetValue = 211,

		/// <summary>FIX XmlDataLen tag (tag 212).</summary>
		FIX_XmlDataLen = 212,

		/// <summary>FIX XmlData tag (tag 213).</summary>
		FIX_XmlData = 213,

		/// <summary>FIX SettlInstRefID tag (tag 214).</summary>
		FIX_SettlInstRefID = 214,

		/// <summary>FIX NoRoutingIDs tag (tag 215).</summary>
		FIX_NoRoutingIDs = 215,

		/// <summary>FIX RoutingType tag (tag 216).</summary>
		FIX_RoutingType = 216,

		/// <summary>FIX RoutingID tag (tag 217).</summary>
		FIX_RoutingID = 217,

		/// <summary>FIX Spread tag (tag 218).</summary>
		FIX_Spread = 218,

		/// <summary>FIX BenchmarkCurveCurrency tag (tag 220).</summary>
		FIX_BenchmarkCurveCurrency = 220,

		/// <summary>FIX BenchmarkCurveName tag (tag 221).</summary>
		FIX_BenchmarkCurveName = 221,

		/// <summary>FIX BenchmarkCurvePoint tag (tag 222).</summary>
		FIX_BenchmarkCurvePoint = 222,

		/// <summary>FIX CouponRate tag (tag 223).</summary>
		FIX_CouponRate = 223,

		/// <summary>FIX CouponPaymentDate tag (tag 224).</summary>
		FIX_CouponPaymentDate = 224,

		/// <summary>FIX IssueDate tag (tag 225).</summary>
		FIX_IssueDate = 225,

		/// <summary>FIX RepurchaseTerm tag (tag 226).</summary>
		FIX_RepurchaseTerm = 226,

		/// <summary>FIX RepurchaseRate tag (tag 227).</summary>
		FIX_RepurchaseRate = 227,

		/// <summary>FIX Factor tag (tag 228).</summary>
		FIX_Factor = 228,

		/// <summary>FIX TradeOriginationDate tag (tag 229).</summary>
		FIX_TradeOriginationDate = 229,

		/// <summary>FIX ExDate tag (tag 230).</summary>
		FIX_ExDate = 230,

		/// <summary>FIX ContractMultiplier tag (tag 231).</summary>
		FIX_ContractMultiplier = 231,

		/// <summary>FIX NoStipulations tag (tag 232).</summary>
		FIX_NoStipulations = 232,

		/// <summary>FIX StipulationType tag (tag 233).</summary>
		FIX_StipulationType = 233,

		/// <summary>FIX StipulationValue tag (tag 234).</summary>
		FIX_StipulationValue = 234,

		/// <summary>FIX YieldType tag (tag 235).</summary>
		FIX_YieldType = 235,

		/// <summary>FIX Yield tag (tag 236).</summary>
		FIX_Yield = 236,

		/// <summary>FIX TotalTakedown tag (tag 237).</summary>
		FIX_TotalTakedown = 237,

		/// <summary>FIX Concession tag (tag 238).</summary>
		FIX_Concession = 238,

		/// <summary>FIX RepoCollateralSecurityType tag (tag 239).</summary>
		FIX_RepoCollateralSecurityType = 239,

		/// <summary>FIX RedemptionDate tag (tag 240).</summary>
		FIX_RedemptionDate = 240,

		/// <summary>FIX UnderlyingCouponPaymentDate tag (tag 241).</summary>
		FIX_UnderlyingCouponPaymentDate = 241,

		/// <summary>FIX UnderlyingIssueDate tag (tag 242).</summary>
		FIX_UnderlyingIssueDate = 242,

		/// <summary>FIX UnderlyingRepoCollateralSecurityType tag (tag 243).</summary>
		FIX_UnderlyingRepoCollateralSecurityType = 243,

		/// <summary>FIX UnderlyingRepurchaseTerm tag (tag 244).</summary>
		FIX_UnderlyingRepurchaseTerm = 244,

		/// <summary>FIX UnderlyingRepurchaseRate tag (tag 245).</summary>
		FIX_UnderlyingRepurchaseRate = 245,

		/// <summary>FIX UnderlyingFactor tag (tag 246).</summary>
		FIX_UnderlyingFactor = 246,

		/// <summary>FIX UnderlyingRedemptionDate tag (tag 247).</summary>
		FIX_UnderlyingRedemptionDate = 247,

		/// <summary>FIX LegCouponPaymentDate tag (tag 248).</summary>
		FIX_LegCouponPaymentDate = 248,

		/// <summary>FIX LegIssueDate tag (tag 249).</summary>
		FIX_LegIssueDate = 249,

		/// <summary>FIX LegRepoCollateralSecurityType tag (tag 250).</summary>
		FIX_LegRepoCollateralSecurityType = 250,

		/// <summary>FIX LegRepurchaseTerm tag (tag 251).</summary>
		FIX_LegRepurchaseTerm = 251,

		/// <summary>FIX LegRepurchaseRate tag (tag 252).</summary>
		FIX_LegRepurchaseRate = 252,

		/// <summary>FIX LegFactor tag (tag 253).</summary>
		FIX_LegFactor = 253,

		/// <summary>FIX LegRedemptionDate tag (tag 254).</summary>
		FIX_LegRedemptionDate = 254,

		/// <summary>FIX CreditRating tag (tag 255).</summary>
		FIX_CreditRating = 255,

		/// <summary>FIX UnderlyingCreditRating tag (tag 256).</summary>
		FIX_UnderlyingCreditRating = 256,

		/// <summary>FIX LegCreditRating tag (tag 257).</summary>
		FIX_LegCreditRating = 257,

		/// <summary>FIX TradedFlatSwitch tag (tag 258).</summary>
		FIX_TradedFlatSwitch = 258,

		/// <summary>FIX BasisFeatureDate tag (tag 259).</summary>
		FIX_BasisFeatureDate = 259,

		/// <summary>FIX BasisFeaturePrice tag (tag 260).</summary>
		FIX_BasisFeaturePrice = 260,

		/// <summary>FIX MDReqID tag (tag 262).</summary>
		FIX_MDReqID = 262,

		/// <summary>FIX SubscriptionRequestType tag (tag 263).</summary>
		FIX_SubscriptionRequestType = 263,

		/// <summary>FIX MarketDepth tag (tag 264).</summary>
		FIX_MarketDepth = 264,

		/// <summary>FIX MDUpdateType tag (tag 265).</summary>
		FIX_MDUpdateType = 265,

		/// <summary>FIX AggregatedBook tag (tag 266).</summary>
		FIX_AggregatedBook = 266,

		/// <summary>FIX NoMDEntryTypes tag (tag 267).</summary>
		FIX_NoMDEntryTypes = 267,

		/// <summary>FIX NoMDEntries tag (tag 268).</summary>
		FIX_NoMDEntries = 268,

		/// <summary>FIX MDEntryType tag (tag 269).</summary>
		FIX_MDEntryType = 269,

		/// <summary>FIX MDEntryPx tag (tag 270).</summary>
		FIX_MDEntryPx = 270,

		/// <summary>FIX MDEntrySize tag (tag 271).</summary>
		FIX_MDEntrySize = 271,

		/// <summary>FIX MDEntryDate tag (tag 272).</summary>
		FIX_MDEntryDate = 272,

		/// <summary>FIX MDEntryTime tag (tag 273).</summary>
		FIX_MDEntryTime = 273,

		/// <summary>FIX TickDirection tag (tag 274).</summary>
		FIX_TickDirection = 274,

		/// <summary>FIX MDMkt tag (tag 275).</summary>
		FIX_MDMkt = 275,

		/// <summary>FIX QuoteCondition tag (tag 276).</summary>
		FIX_QuoteCondition = 276,

		/// <summary>FIX TradeCondition tag (tag 277).</summary>
		FIX_TradeCondition = 277,

		/// <summary>FIX MDEntryID tag (tag 278).</summary>
		FIX_MDEntryID = 278,

		/// <summary>FIX MDUpdateAction tag (tag 279).</summary>
		FIX_MDUpdateAction = 279,

		/// <summary>FIX MDEntryRefID tag (tag 280).</summary>
		FIX_MDEntryRefID = 280,

		/// <summary>FIX MDReqRejReason tag (tag 281).</summary>
		FIX_MDReqRejReason = 281,

		/// <summary>FIX MDEntryOriginator tag (tag 282).</summary>
		FIX_MDEntryOriginator = 282,

		/// <summary>FIX LocationID tag (tag 283).</summary>
		FIX_LocationID = 283,

		/// <summary>FIX DeskID tag (tag 284).</summary>
		FIX_DeskID = 284,

		/// <summary>FIX DeleteReason tag (tag 285).</summary>
		FIX_DeleteReason = 285,

		/// <summary>FIX OpenCloseSettlFlag tag (tag 286).</summary>
		FIX_OpenCloseSettlFlag = 286,

		/// <summary>FIX SellerDays tag (tag 287).</summary>
		FIX_SellerDays = 287,

		/// <summary>FIX MDEntryBuyer tag (tag 288).</summary>
		FIX_MDEntryBuyer = 288,

		/// <summary>FIX MDEntrySeller tag (tag 289).</summary>
		FIX_MDEntrySeller = 289,

		/// <summary>FIX MDEntryPositionNo tag (tag 290).</summary>
		FIX_MDEntryPositionNo = 290,

		/// <summary>FIX FinancialStatus tag (tag 291).</summary>
		FIX_FinancialStatus = 291,

		/// <summary>FIX CorporateAction tag (tag 292).</summary>
		FIX_CorporateAction = 292,

		/// <summary>FIX DefBidSize tag (tag 293).</summary>
		FIX_DefBidSize = 293,

		/// <summary>FIX DefOfferSize tag (tag 294).</summary>
		FIX_DefOfferSize = 294,

		/// <summary>FIX NoQuoteEntries tag (tag 295).</summary>
		FIX_NoQuoteEntries = 295,

		/// <summary>FIX NoQuoteSets tag (tag 296).</summary>
		FIX_NoQuoteSets = 296,

		/// <summary>FIX QuoteStatus tag (tag 297).</summary>
		FIX_QuoteStatus = 297,

		/// <summary>FIX QuoteCancelType tag (tag 298).</summary>
		FIX_QuoteCancelType = 298,

		/// <summary>FIX QuoteEntryID tag (tag 299).</summary>
		FIX_QuoteEntryID = 299,

		/// <summary>FIX QuoteRejectReason tag (tag 300).</summary>
		FIX_QuoteRejectReason = 300,

		/// <summary>FIX QuoteResponseLevel tag (tag 301).</summary>
		FIX_QuoteResponseLevel = 301,

		/// <summary>FIX QuoteSetID tag (tag 302).</summary>
		FIX_QuoteSetID = 302,

		/// <summary>FIX QuoteRequestType tag (tag 303).</summary>
		FIX_QuoteRequestType = 303,

		/// <summary>FIX TotNoQuoteEntries tag (tag 304).</summary>
		FIX_TotNoQuoteEntries = 304,

		/// <summary>FIX UnderlyingSecurityIDSource tag (tag 305).</summary>
		FIX_UnderlyingSecurityIDSource = 305,

		/// <summary>FIX UnderlyingIssuer tag (tag 306).</summary>
		FIX_UnderlyingIssuer = 306,

		/// <summary>FIX UnderlyingSecurityDesc tag (tag 307).</summary>
		FIX_UnderlyingSecurityDesc = 307,

		/// <summary>FIX UnderlyingSecurityExchange tag (tag 308).</summary>
		FIX_UnderlyingSecurityExchange = 308,

		/// <summary>FIX UnderlyingSecurityID tag (tag 309).</summary>
		FIX_UnderlyingSecurityID = 309,

		/// <summary>FIX UnderlyingSecurityType tag (tag 310).</summary>
		FIX_UnderlyingSecurityType = 310,

		/// <summary>FIX UnderlyingSymbol tag (tag 311).</summary>
		FIX_UnderlyingSymbol = 311,

		/// <summary>FIX UnderlyingSymbolSfx tag (tag 312).</summary>
		FIX_UnderlyingSymbolSfx = 312,

		/// <summary>FIX UnderlyingMaturityMonthYear tag (tag 313).</summary>
		FIX_UnderlyingMaturityMonthYear = 313,

		/// <summary>FIX UnderlyingPutOrCall tag (tag 315).</summary>
		FIX_UnderlyingPutOrCall = 315,

		/// <summary>FIX UnderlyingStrikePrice tag (tag 316).</summary>
		FIX_UnderlyingStrikePrice = 316,

		/// <summary>FIX UnderlyingOptAttribute tag (tag 317).</summary>
		FIX_UnderlyingOptAttribute = 317,

		/// <summary>FIX UnderlyingCurrency tag (tag 318).</summary>
		FIX_UnderlyingCurrency = 318,

		/// <summary>FIX SecurityReqID tag (tag 320).</summary>
		FIX_SecurityReqID = 320,

		/// <summary>FIX SecurityRequestType tag (tag 321).</summary>
		FIX_SecurityRequestType = 321,

		/// <summary>FIX SecurityResponseID tag (tag 322).</summary>
		FIX_SecurityResponseID = 322,

		/// <summary>FIX SecurityResponseType tag (tag 323).</summary>
		FIX_SecurityResponseType = 323,

		/// <summary>FIX SecurityStatusReqID tag (tag 324).</summary>
		FIX_SecurityStatusReqID = 324,

		/// <summary>FIX UnsolicitedIndicator tag (tag 325).</summary>
		FIX_UnsolicitedIndicator = 325,

		/// <summary>FIX SecurityTradingStatus tag (tag 326).</summary>
		FIX_SecurityTradingStatus = 326,

		/// <summary>FIX HaltReason tag (tag 327).</summary>
		FIX_HaltReason = 327,

		/// <summary>FIX InViewOfCommon tag (tag 328).</summary>
		FIX_InViewOfCommon = 328,

		/// <summary>FIX DueToRelated tag (tag 329).</summary>
		FIX_DueToRelated = 329,

		/// <summary>FIX BuyVolume tag (tag 330).</summary>
		FIX_BuyVolume = 330,

		/// <summary>FIX SellVolume tag (tag 331).</summary>
		FIX_SellVolume = 331,

		/// <summary>FIX HighPx tag (tag 332).</summary>
		FIX_HighPx = 332,

		/// <summary>FIX LowPx tag (tag 333).</summary>
		FIX_LowPx = 333,

		/// <summary>FIX Adjustment tag (tag 334).</summary>
		FIX_Adjustment = 334,

		/// <summary>FIX TradSesReqID tag (tag 335).</summary>
		FIX_TradSesReqID = 335,

		/// <summary>FIX TradingSessionID tag (tag 336).</summary>
		FIX_TradingSessionID = 336,

		/// <summary>FIX ContraTrader tag (tag 337).</summary>
		FIX_ContraTrader = 337,

		/// <summary>FIX TradSesMethod tag (tag 338).</summary>
		FIX_TradSesMethod = 338,

		/// <summary>FIX TradSesMode tag (tag 339).</summary>
		FIX_TradSesMode = 339,

		/// <summary>FIX TradSesStatus tag (tag 340).</summary>
		FIX_TradSesStatus = 340,

		/// <summary>FIX TradSesStartTime tag (tag 341).</summary>
		FIX_TradSesStartTime = 341,

		/// <summary>FIX TradSesOpenTime tag (tag 342).</summary>
		FIX_TradSesOpenTime = 342,

		/// <summary>FIX TradSesPreCloseTime tag (tag 343).</summary>
		FIX_TradSesPreCloseTime = 343,

		/// <summary>FIX TradSesCloseTime tag (tag 344).</summary>
		FIX_TradSesCloseTime = 344,

		/// <summary>FIX TradSesEndTime tag (tag 345).</summary>
		FIX_TradSesEndTime = 345,

		/// <summary>FIX NumberOfOrders tag (tag 346).</summary>
		FIX_NumberOfOrders = 346,

		/// <summary>FIX MessageEncoding tag (tag 347).</summary>
		FIX_MessageEncoding = 347,

		/// <summary>FIX EncodedIssuerLen tag (tag 348).</summary>
		FIX_EncodedIssuerLen = 348,

		/// <summary>FIX EncodedIssuer tag (tag 349).</summary>
		FIX_EncodedIssuer = 349,

		/// <summary>FIX EncodedSecurityDescLen tag (tag 350).</summary>
		FIX_EncodedSecurityDescLen = 350,

		/// <summary>FIX EncodedSecurityDesc tag (tag 351).</summary>
		FIX_EncodedSecurityDesc = 351,

		/// <summary>FIX EncodedListExecInstLen tag (tag 352).</summary>
		FIX_EncodedListExecInstLen = 352,

		/// <summary>FIX EncodedListExecInst tag (tag 353).</summary>
		FIX_EncodedListExecInst = 353,

		/// <summary>FIX EncodedTextLen tag (tag 354).</summary>
		FIX_EncodedTextLen = 354,

		/// <summary>FIX EncodedText tag (tag 355).</summary>
		FIX_EncodedText = 355,

		/// <summary>FIX EncodedSubjectLen tag (tag 356).</summary>
		FIX_EncodedSubjectLen = 356,

		/// <summary>FIX EncodedSubject tag (tag 357).</summary>
		FIX_EncodedSubject = 357,

		/// <summary>FIX EncodedHeadlineLen tag (tag 358).</summary>
		FIX_EncodedHeadlineLen = 358,

		/// <summary>FIX EncodedHeadline tag (tag 359).</summary>
		FIX_EncodedHeadline = 359,

		/// <summary>FIX EncodedAllocTextLen tag (tag 360).</summary>
		FIX_EncodedAllocTextLen = 360,

		/// <summary>FIX EncodedAllocText tag (tag 361).</summary>
		FIX_EncodedAllocText = 361,

		/// <summary>FIX EncodedUnderlyingIssuerLen tag (tag 362).</summary>
		FIX_EncodedUnderlyingIssuerLen = 362,

		/// <summary>FIX EncodedUnderlyingIssuer tag (tag 363).</summary>
		FIX_EncodedUnderlyingIssuer = 363,

		/// <summary>FIX EncodedUnderlyingSecurityDescLen tag (tag 364).</summary>
		FIX_EncodedUnderlyingSecurityDescLen = 364,

		/// <summary>FIX EncodedUnderlyingSecurityDesc tag (tag 365).</summary>
		FIX_EncodedUnderlyingSecurityDesc = 365,

		/// <summary>FIX AllocPrice tag (tag 366).</summary>
		FIX_AllocPrice = 366,

		/// <summary>FIX QuoteSetValidUntilTime tag (tag 367).</summary>
		FIX_QuoteSetValidUntilTime = 367,

		/// <summary>FIX QuoteEntryRejectReason tag (tag 368).</summary>
		FIX_QuoteEntryRejectReason = 368,

		/// <summary>FIX LastMsgSeqNumProcessed tag (tag 369).</summary>
		FIX_LastMsgSeqNumProcessed = 369,

		/// <summary>FIX RefTagID tag (tag 371).</summary>
		FIX_RefTagID = 371,

		/// <summary>FIX RefMsgType tag (tag 372).</summary>
		FIX_RefMsgType = 372,

		/// <summary>FIX SessionRejectReason tag (tag 373).</summary>
		FIX_SessionRejectReason = 373,

		/// <summary>FIX BidRequestTransType tag (tag 374).</summary>
		FIX_BidRequestTransType = 374,

		/// <summary>FIX ContraBroker tag (tag 375).</summary>
		FIX_ContraBroker = 375,

		/// <summary>FIX ComplianceID tag (tag 376).</summary>
		FIX_ComplianceID = 376,

		/// <summary>FIX SolicitedFlag tag (tag 377).</summary>
		FIX_SolicitedFlag = 377,

		/// <summary>FIX ExecRestatementReason tag (tag 378).</summary>
		FIX_ExecRestatementReason = 378,

		/// <summary>FIX BusinessRejectRefID tag (tag 379).</summary>
		FIX_BusinessRejectRefID = 379,

		/// <summary>FIX BusinessRejectReason tag (tag 380).</summary>
		FIX_BusinessRejectReason = 380,

		/// <summary>FIX GrossTradeAmt tag (tag 381).</summary>
		FIX_GrossTradeAmt = 381,

		/// <summary>FIX NoContraBrokers tag (tag 382).</summary>
		FIX_NoContraBrokers = 382,

		/// <summary>FIX MaxMessageSize tag (tag 383).</summary>
		FIX_MaxMessageSize = 383,

		/// <summary>FIX NoMsgTypes tag (tag 384).</summary>
		FIX_NoMsgTypes = 384,

		/// <summary>FIX MsgDirection tag (tag 385).</summary>
		FIX_MsgDirection = 385,

		/// <summary>FIX NoTradingSessions tag (tag 386).</summary>
		FIX_NoTradingSessions = 386,

		/// <summary>FIX TotalVolumeTraded tag (tag 387).</summary>
		FIX_TotalVolumeTraded = 387,

		/// <summary>FIX DiscretionInst tag (tag 388).</summary>
		FIX_DiscretionInst = 388,

		/// <summary>FIX DiscretionOffsetValue tag (tag 389).</summary>
		FIX_DiscretionOffsetValue = 389,

		/// <summary>FIX BidID tag (tag 390).</summary>
		FIX_BidID = 390,

		/// <summary>FIX ClientBidID tag (tag 391).</summary>
		FIX_ClientBidID = 391,

		/// <summary>FIX ListName tag (tag 392).</summary>
		FIX_ListName = 392,

		/// <summary>FIX TotNoRelatedSym tag (tag 393).</summary>
		FIX_TotNoRelatedSym = 393,

		/// <summary>FIX BidType tag (tag 394).</summary>
		FIX_BidType = 394,

		/// <summary>FIX NumTickets tag (tag 395).</summary>
		FIX_NumTickets = 395,

		/// <summary>FIX SideValue1 tag (tag 396).</summary>
		FIX_SideValue1 = 396,

		/// <summary>FIX SideValue2 tag (tag 397).</summary>
		FIX_SideValue2 = 397,

		/// <summary>FIX NoBidDescriptors tag (tag 398).</summary>
		FIX_NoBidDescriptors = 398,

		/// <summary>FIX BidDescriptorType tag (tag 399).</summary>
		FIX_BidDescriptorType = 399,

		/// <summary>FIX BidDescriptor tag (tag 400).</summary>
		FIX_BidDescriptor = 400,

		/// <summary>FIX SideValueInd tag (tag 401).</summary>
		FIX_SideValueInd = 401,

		/// <summary>FIX LiquidityPctLow tag (tag 402).</summary>
		FIX_LiquidityPctLow = 402,

		/// <summary>FIX LiquidityPctHigh tag (tag 403).</summary>
		FIX_LiquidityPctHigh = 403,

		/// <summary>FIX LiquidityValue tag (tag 404).</summary>
		FIX_LiquidityValue = 404,

		/// <summary>FIX EFPTrackingError tag (tag 405).</summary>
		FIX_EFPTrackingError = 405,

		/// <summary>FIX FairValue tag (tag 406).</summary>
		FIX_FairValue = 406,

		/// <summary>FIX OutsideIndexPct tag (tag 407).</summary>
		FIX_OutsideIndexPct = 407,

		/// <summary>FIX ValueOfFutures tag (tag 408).</summary>
		FIX_ValueOfFutures = 408,

		/// <summary>FIX LiquidityIndType tag (tag 409).</summary>
		FIX_LiquidityIndType = 409,

		/// <summary>FIX WtAverageLiquidity tag (tag 410).</summary>
		FIX_WtAverageLiquidity = 410,

		/// <summary>FIX ExchangeForPhysical tag (tag 411).</summary>
		FIX_ExchangeForPhysical = 411,

		/// <summary>FIX OutMainCntryUIndex tag (tag 412).</summary>
		FIX_OutMainCntryUIndex = 412,

		/// <summary>FIX CrossPercent tag (tag 413).</summary>
		FIX_CrossPercent = 413,

		/// <summary>FIX ProgRptReqs tag (tag 414).</summary>
		FIX_ProgRptReqs = 414,

		/// <summary>FIX ProgPeriodInterval tag (tag 415).</summary>
		FIX_ProgPeriodInterval = 415,

		/// <summary>FIX IncTaxInd tag (tag 416).</summary>
		FIX_IncTaxInd = 416,

		/// <summary>FIX NumBidders tag (tag 417).</summary>
		FIX_NumBidders = 417,

		/// <summary>FIX BidTradeType tag (tag 418).</summary>
		FIX_BidTradeType = 418,

		/// <summary>FIX BasisPxType tag (tag 419).</summary>
		FIX_BasisPxType = 419,

		/// <summary>FIX NoBidComponents tag (tag 420).</summary>
		FIX_NoBidComponents = 420,

		/// <summary>FIX Country tag (tag 421).</summary>
		FIX_Country = 421,

		/// <summary>FIX TotNoStrikes tag (tag 422).</summary>
		FIX_TotNoStrikes = 422,

		/// <summary>FIX PriceType tag (tag 423).</summary>
		FIX_PriceType = 423,

		/// <summary>FIX DayOrderQty tag (tag 424).</summary>
		FIX_DayOrderQty = 424,

		/// <summary>FIX DayCumQty tag (tag 425).</summary>
		FIX_DayCumQty = 425,

		/// <summary>FIX DayAvgPx tag (tag 426).</summary>
		FIX_DayAvgPx = 426,

		/// <summary>FIX GTBookingInst tag (tag 427).</summary>
		FIX_GTBookingInst = 427,

		/// <summary>FIX NoStrikes tag (tag 428).</summary>
		FIX_NoStrikes = 428,

		/// <summary>FIX ListStatusType tag (tag 429).</summary>
		FIX_ListStatusType = 429,

		/// <summary>FIX NetGrossInd tag (tag 430).</summary>
		FIX_NetGrossInd = 430,

		/// <summary>FIX ListOrderStatus tag (tag 431).</summary>
		FIX_ListOrderStatus = 431,

		/// <summary>FIX ExpireDate tag (tag 432).</summary>
		FIX_ExpireDate = 432,

		/// <summary>FIX ListExecInstType tag (tag 433).</summary>
		FIX_ListExecInstType = 433,

		/// <summary>FIX CxlRejResponseTo tag (tag 434).</summary>
		FIX_CxlRejResponseTo = 434,

		/// <summary>FIX UnderlyingCouponRate tag (tag 435).</summary>
		FIX_UnderlyingCouponRate = 435,

		/// <summary>FIX UnderlyingContractMultiplier tag (tag 436).</summary>
		FIX_UnderlyingContractMultiplier = 436,

		/// <summary>FIX ContraTradeQty tag (tag 437).</summary>
		FIX_ContraTradeQty = 437,

		/// <summary>FIX ContraTradeTime tag (tag 438).</summary>
		FIX_ContraTradeTime = 438,

		/// <summary>FIX LiquidityNumSecurities tag (tag 441).</summary>
		FIX_LiquidityNumSecurities = 441,

		/// <summary>FIX MultiLegReportingType tag (tag 442).</summary>
		FIX_MultiLegReportingType = 442,

		/// <summary>FIX StrikeTime tag (tag 443).</summary>
		FIX_StrikeTime = 443,

		/// <summary>FIX ListStatusText tag (tag 444).</summary>
		FIX_ListStatusText = 444,

		/// <summary>FIX EncodedListStatusTextLen tag (tag 445).</summary>
		FIX_EncodedListStatusTextLen = 445,

		/// <summary>FIX EncodedListStatusText tag (tag 446).</summary>
		FIX_EncodedListStatusText = 446,

		/// <summary>FIX PartyIDSource tag (tag 447).</summary>
		FIX_PartyIDSource = 447,

		/// <summary>FIX PartyID tag (tag 448).</summary>
		FIX_PartyID = 448,

		/// <summary>FIX NetChgPrevDay tag (tag 451).</summary>
		FIX_NetChgPrevDay = 451,

		/// <summary>FIX PartyRole tag (tag 452).</summary>
		FIX_PartyRole = 452,

		/// <summary>FIX NoPartyIDs tag (tag 453).</summary>
		FIX_NoPartyIDs = 453,

		/// <summary>FIX NoSecurityAltID tag (tag 454).</summary>
		FIX_NoSecurityAltID = 454,

		/// <summary>FIX SecurityAltID tag (tag 455).</summary>
		FIX_SecurityAltID = 455,

		/// <summary>FIX SecurityAltIDSource tag (tag 456).</summary>
		FIX_SecurityAltIDSource = 456,

		/// <summary>FIX NoUnderlyingSecurityAltID tag (tag 457).</summary>
		FIX_NoUnderlyingSecurityAltID = 457,

		/// <summary>FIX UnderlyingSecurityAltID tag (tag 458).</summary>
		FIX_UnderlyingSecurityAltID = 458,

		/// <summary>FIX UnderlyingSecurityAltIDSource tag (tag 459).</summary>
		FIX_UnderlyingSecurityAltIDSource = 459,

		/// <summary>FIX Product tag (tag 460).</summary>
		FIX_Product = 460,

		/// <summary>FIX CFICode tag (tag 461).</summary>
		FIX_CFICode = 461,

		/// <summary>FIX UnderlyingProduct tag (tag 462).</summary>
		FIX_UnderlyingProduct = 462,

		/// <summary>FIX UnderlyingCFICode tag (tag 463).</summary>
		FIX_UnderlyingCFICode = 463,

		/// <summary>FIX TestMessageIndicator tag (tag 464).</summary>
		FIX_TestMessageIndicator = 464,

		/// <summary>FIX BookingRefID tag (tag 466).</summary>
		FIX_BookingRefID = 466,

		/// <summary>FIX IndividualAllocID tag (tag 467).</summary>
		FIX_IndividualAllocID = 467,

		/// <summary>FIX RoundingDirection tag (tag 468).</summary>
		FIX_RoundingDirection = 468,

		/// <summary>FIX RoundingModulus tag (tag 469).</summary>
		FIX_RoundingModulus = 469,

		/// <summary>FIX CountryOfIssue tag (tag 470).</summary>
		FIX_CountryOfIssue = 470,

		/// <summary>FIX StateOrProvinceOfIssue tag (tag 471).</summary>
		FIX_StateOrProvinceOfIssue = 471,

		/// <summary>FIX LocaleOfIssue tag (tag 472).</summary>
		FIX_LocaleOfIssue = 472,

		/// <summary>FIX NoRegistDtls tag (tag 473).</summary>
		FIX_NoRegistDtls = 473,

		/// <summary>FIX MailingDtls tag (tag 474).</summary>
		FIX_MailingDtls = 474,

		/// <summary>FIX InvestorCountryOfResidence tag (tag 475).</summary>
		FIX_InvestorCountryOfResidence = 475,

		/// <summary>FIX PaymentRef tag (tag 476).</summary>
		FIX_PaymentRef = 476,

		/// <summary>FIX DistribPaymentMethod tag (tag 477).</summary>
		FIX_DistribPaymentMethod = 477,

		/// <summary>FIX CashDistribCurr tag (tag 478).</summary>
		FIX_CashDistribCurr = 478,

		/// <summary>FIX CommCurrency tag (tag 479).</summary>
		FIX_CommCurrency = 479,

		/// <summary>FIX CancellationRights tag (tag 480).</summary>
		FIX_CancellationRights = 480,

		/// <summary>FIX MoneyLaunderingStatus tag (tag 481).</summary>
		FIX_MoneyLaunderingStatus = 481,

		/// <summary>FIX MailingInst tag (tag 482).</summary>
		FIX_MailingInst = 482,

		/// <summary>FIX TransBkdTime tag (tag 483).</summary>
		FIX_TransBkdTime = 483,

		/// <summary>FIX ExecPriceType tag (tag 484).</summary>
		FIX_ExecPriceType = 484,

		/// <summary>FIX ExecPriceAdjustment tag (tag 485).</summary>
		FIX_ExecPriceAdjustment = 485,

		/// <summary>FIX DateOfBirth tag (tag 486).</summary>
		FIX_DateOfBirth = 486,

		/// <summary>FIX TradeReportTransType tag (tag 487).</summary>
		FIX_TradeReportTransType = 487,

		/// <summary>FIX CardHolderName tag (tag 488).</summary>
		FIX_CardHolderName = 488,

		/// <summary>FIX CardNumber tag (tag 489).</summary>
		FIX_CardNumber = 489,

		/// <summary>FIX CardExpDate tag (tag 490).</summary>
		FIX_CardExpDate = 490,

		/// <summary>FIX CardIssNum tag (tag 491).</summary>
		FIX_CardIssNum = 491,

		/// <summary>FIX PaymentMethod tag (tag 492).</summary>
		FIX_PaymentMethod = 492,

		/// <summary>FIX RegistAcctType tag (tag 493).</summary>
		FIX_RegistAcctType = 493,

		/// <summary>FIX Designation tag (tag 494).</summary>
		FIX_Designation = 494,

		/// <summary>FIX TaxAdvantageType tag (tag 495).</summary>
		FIX_TaxAdvantageType = 495,

		/// <summary>FIX RegistRejReasonText tag (tag 496).</summary>
		FIX_RegistRejReasonText = 496,

		/// <summary>FIX FundRenewWaiv tag (tag 497).</summary>
		FIX_FundRenewWaiv = 497,

		/// <summary>FIX CashDistribAgentName tag (tag 498).</summary>
		FIX_CashDistribAgentName = 498,

		/// <summary>FIX CashDistribAgentCode tag (tag 499).</summary>
		FIX_CashDistribAgentCode = 499,

		/// <summary>FIX CashDistribAgentAcctNumber tag (tag 500).</summary>
		FIX_CashDistribAgentAcctNumber = 500,

		/// <summary>FIX CashDistribPayRef tag (tag 501).</summary>
		FIX_CashDistribPayRef = 501,

		/// <summary>FIX CashDistribAgentAcctName tag (tag 502).</summary>
		FIX_CashDistribAgentAcctName = 502,

		/// <summary>FIX CardStartDate tag (tag 503).</summary>
		FIX_CardStartDate = 503,

		/// <summary>FIX PaymentDate tag (tag 504).</summary>
		FIX_PaymentDate = 504,

		/// <summary>FIX PaymentRemitterID tag (tag 505).</summary>
		FIX_PaymentRemitterID = 505,

		/// <summary>FIX RegistStatus tag (tag 506).</summary>
		FIX_RegistStatus = 506,

		/// <summary>FIX RegistRejReasonCode tag (tag 507).</summary>
		FIX_RegistRejReasonCode = 507,

		/// <summary>FIX RegistRefID tag (tag 508).</summary>
		FIX_RegistRefID = 508,

		/// <summary>FIX RegistDtls tag (tag 509).</summary>
		FIX_RegistDtls = 509,

		/// <summary>FIX NoDistribInsts tag (tag 510).</summary>
		FIX_NoDistribInsts = 510,

		/// <summary>FIX RegistEmail tag (tag 511).</summary>
		FIX_RegistEmail = 511,

		/// <summary>FIX DistribPercentage tag (tag 512).</summary>
		FIX_DistribPercentage = 512,

		/// <summary>FIX RegistID tag (tag 513).</summary>
		FIX_RegistID = 513,

		/// <summary>FIX RegistTransType tag (tag 514).</summary>
		FIX_RegistTransType = 514,

		/// <summary>FIX ExecValuationPoint tag (tag 515).</summary>
		FIX_ExecValuationPoint = 515,

		/// <summary>FIX OrderPercent tag (tag 516).</summary>
		FIX_OrderPercent = 516,

		/// <summary>FIX OwnershipType tag (tag 517).</summary>
		FIX_OwnershipType = 517,

		/// <summary>FIX NoContAmts tag (tag 518).</summary>
		FIX_NoContAmts = 518,

		/// <summary>FIX ContAmtType tag (tag 519).</summary>
		FIX_ContAmtType = 519,

		/// <summary>FIX ContAmtValue tag (tag 520).</summary>
		FIX_ContAmtValue = 520,

		/// <summary>FIX ContAmtCurr tag (tag 521).</summary>
		FIX_ContAmtCurr = 521,

		/// <summary>FIX OwnerType tag (tag 522).</summary>
		FIX_OwnerType = 522,

		/// <summary>FIX PartySubID tag (tag 523).</summary>
		FIX_PartySubID = 523,

		/// <summary>FIX NestedPartyID tag (tag 524).</summary>
		FIX_NestedPartyID = 524,

		/// <summary>FIX NestedPartyIDSource tag (tag 525).</summary>
		FIX_NestedPartyIDSource = 525,

		/// <summary>FIX SecondaryClOrdID tag (tag 526).</summary>
		FIX_SecondaryClOrdID = 526,

		/// <summary>FIX SecondaryExecID tag (tag 527).</summary>
		FIX_SecondaryExecID = 527,

		/// <summary>FIX OrderCapacity tag (tag 528).</summary>
		FIX_OrderCapacity = 528,

		/// <summary>FIX OrderRestrictions tag (tag 529).</summary>
		FIX_OrderRestrictions = 529,

		/// <summary>FIX MassCancelRequestType tag (tag 530).</summary>
		FIX_MassCancelRequestType = 530,

		/// <summary>FIX MassCancelResponse tag (tag 531).</summary>
		FIX_MassCancelResponse = 531,

		/// <summary>FIX MassCancelRejectReason tag (tag 532).</summary>
		FIX_MassCancelRejectReason = 532,

		/// <summary>FIX TotalAffectedOrders tag (tag 533).</summary>
		FIX_TotalAffectedOrders = 533,

		/// <summary>FIX NoAffectedOrders tag (tag 534).</summary>
		FIX_NoAffectedOrders = 534,

		/// <summary>FIX AffectedOrderID tag (tag 535).</summary>
		FIX_AffectedOrderID = 535,

		/// <summary>FIX AffectedSecondaryOrderID tag (tag 536).</summary>
		FIX_AffectedSecondaryOrderID = 536,

		/// <summary>FIX QuoteType tag (tag 537).</summary>
		FIX_QuoteType = 537,

		/// <summary>FIX NestedPartyRole tag (tag 538).</summary>
		FIX_NestedPartyRole = 538,

		/// <summary>FIX NoNestedPartyIDs tag (tag 539).</summary>
		FIX_NoNestedPartyIDs = 539,

		/// <summary>FIX TotalAccruedInterestAmt tag (tag 540).</summary>
		FIX_TotalAccruedInterestAmt = 540,

		/// <summary>FIX MaturityDate tag (tag 541).</summary>
		FIX_MaturityDate = 541,

		/// <summary>FIX UnderlyingMaturityDate tag (tag 542).</summary>
		FIX_UnderlyingMaturityDate = 542,

		/// <summary>FIX InstrRegistry tag (tag 543).</summary>
		FIX_InstrRegistry = 543,

		/// <summary>FIX CashMargin tag (tag 544).</summary>
		FIX_CashMargin = 544,

		/// <summary>FIX NestedPartySubID tag (tag 545).</summary>
		FIX_NestedPartySubID = 545,

		/// <summary>FIX Scope tag (tag 546).</summary>
		FIX_Scope = 546,

		/// <summary>FIX MDImplicitDelete tag (tag 547).</summary>
		FIX_MDImplicitDelete = 547,

		/// <summary>FIX CrossID tag (tag 548).</summary>
		FIX_CrossID = 548,

		/// <summary>FIX CrossType tag (tag 549).</summary>
		FIX_CrossType = 549,

		/// <summary>FIX CrossPrioritization tag (tag 550).</summary>
		FIX_CrossPrioritization = 550,

		/// <summary>FIX OrigCrossID tag (tag 551).</summary>
		FIX_OrigCrossID = 551,

		/// <summary>FIX NoSides tag (tag 552).</summary>
		FIX_NoSides = 552,

		/// <summary>FIX Username tag (tag 553).</summary>
		FIX_Username = 553,

		/// <summary>FIX Password tag (tag 554).</summary>
		FIX_Password = 554,

		/// <summary>FIX NoLegs tag (tag 555).</summary>
		FIX_NoLegs = 555,

		/// <summary>FIX LegCurrency tag (tag 556).</summary>
		FIX_LegCurrency = 556,

		/// <summary>FIX TotNoSecurityTypes tag (tag 557).</summary>
		FIX_TotNoSecurityTypes = 557,

		/// <summary>FIX NoSecurityTypes tag (tag 558).</summary>
		FIX_NoSecurityTypes = 558,

		/// <summary>FIX SecurityListRequestType tag (tag 559).</summary>
		FIX_SecurityListRequestType = 559,

		/// <summary>FIX SecurityRequestResult tag (tag 560).</summary>
		FIX_SecurityRequestResult = 560,

		/// <summary>FIX RoundLot tag (tag 561).</summary>
		FIX_RoundLot = 561,

		/// <summary>FIX MinTradeVol tag (tag 562).</summary>
		FIX_MinTradeVol = 562,

		/// <summary>FIX MultiLegRptTypeReq tag (tag 563).</summary>
		FIX_MultiLegRptTypeReq = 563,

		/// <summary>FIX LegPositionEffect tag (tag 564).</summary>
		FIX_LegPositionEffect = 564,

		/// <summary>FIX LegCoveredOrUncovered tag (tag 565).</summary>
		FIX_LegCoveredOrUncovered = 565,

		/// <summary>FIX LegPrice tag (tag 566).</summary>
		FIX_LegPrice = 566,

		/// <summary>FIX TradSesStatusRejReason tag (tag 567).</summary>
		FIX_TradSesStatusRejReason = 567,

		/// <summary>FIX TradeRequestID tag (tag 568).</summary>
		FIX_TradeRequestID = 568,

		/// <summary>FIX TradeRequestType tag (tag 569).</summary>
		FIX_TradeRequestType = 569,

		/// <summary>FIX PreviouslyReported tag (tag 570).</summary>
		FIX_PreviouslyReported = 570,

		/// <summary>FIX TradeReportID tag (tag 571).</summary>
		FIX_TradeReportID = 571,

		/// <summary>FIX TradeReportRefID tag (tag 572).</summary>
		FIX_TradeReportRefID = 572,

		/// <summary>FIX MatchStatus tag (tag 573).</summary>
		FIX_MatchStatus = 573,

		/// <summary>FIX MatchType tag (tag 574).</summary>
		FIX_MatchType = 574,

		/// <summary>FIX OddLot tag (tag 575).</summary>
		FIX_OddLot = 575,

		/// <summary>FIX NoClearingInstructions tag (tag 576).</summary>
		FIX_NoClearingInstructions = 576,

		/// <summary>FIX ClearingInstruction tag (tag 577).</summary>
		FIX_ClearingInstruction = 577,

		/// <summary>FIX TradeInputSource tag (tag 578).</summary>
		FIX_TradeInputSource = 578,

		/// <summary>FIX TradeInputDevice tag (tag 579).</summary>
		FIX_TradeInputDevice = 579,

		/// <summary>FIX NoDates tag (tag 580).</summary>
		FIX_NoDates = 580,

		/// <summary>FIX AccountType tag (tag 581).</summary>
		FIX_AccountType = 581,

		/// <summary>FIX CustOrderCapacity tag (tag 582).</summary>
		FIX_CustOrderCapacity = 582,

		/// <summary>FIX ClOrdLinkID tag (tag 583).</summary>
		FIX_ClOrdLinkID = 583,

		/// <summary>FIX MassStatusReqID tag (tag 584).</summary>
		FIX_MassStatusReqID = 584,

		/// <summary>FIX MassStatusReqType tag (tag 585).</summary>
		FIX_MassStatusReqType = 585,

		/// <summary>FIX OrigOrdModTime tag (tag 586).</summary>
		FIX_OrigOrdModTime = 586,

		/// <summary>FIX LegSettlType tag (tag 587).</summary>
		FIX_LegSettlType = 587,

		/// <summary>FIX LegSettlDate tag (tag 588).</summary>
		FIX_LegSettlDate = 588,

		/// <summary>FIX DayBookingInst tag (tag 589).</summary>
		FIX_DayBookingInst = 589,

		/// <summary>FIX BookingUnit tag (tag 590).</summary>
		FIX_BookingUnit = 590,

		/// <summary>FIX PreallocMethod tag (tag 591).</summary>
		FIX_PreallocMethod = 591,

		/// <summary>FIX UnderlyingCountryOfIssue tag (tag 592).</summary>
		FIX_UnderlyingCountryOfIssue = 592,

		/// <summary>FIX UnderlyingStateOrProvinceOfIssue tag (tag 593).</summary>
		FIX_UnderlyingStateOrProvinceOfIssue = 593,

		/// <summary>FIX UnderlyingLocaleOfIssue tag (tag 594).</summary>
		FIX_UnderlyingLocaleOfIssue = 594,

		/// <summary>FIX UnderlyingInstrRegistry tag (tag 595).</summary>
		FIX_UnderlyingInstrRegistry = 595,

		/// <summary>FIX LegCountryOfIssue tag (tag 596).</summary>
		FIX_LegCountryOfIssue = 596,

		/// <summary>FIX LegStateOrProvinceOfIssue tag (tag 597).</summary>
		FIX_LegStateOrProvinceOfIssue = 597,

		/// <summary>FIX LegLocaleOfIssue tag (tag 598).</summary>
		FIX_LegLocaleOfIssue = 598,

		/// <summary>FIX LegInstrRegistry tag (tag 599).</summary>
		FIX_LegInstrRegistry = 599,

		/// <summary>FIX LegSymbol tag (tag 600).</summary>
		FIX_LegSymbol = 600,

		/// <summary>FIX LegSymbolSfx tag (tag 601).</summary>
		FIX_LegSymbolSfx = 601,

		/// <summary>FIX LegSecurityID tag (tag 602).</summary>
		FIX_LegSecurityID = 602,

		/// <summary>FIX LegSecurityIDSource tag (tag 603).</summary>
		FIX_LegSecurityIDSource = 603,

		/// <summary>FIX NoLegSecurityAltID tag (tag 604).</summary>
		FIX_NoLegSecurityAltID = 604,

		/// <summary>FIX LegSecurityAltID tag (tag 605).</summary>
		FIX_LegSecurityAltID = 605,

		/// <summary>FIX LegSecurityAltIDSource tag (tag 606).</summary>
		FIX_LegSecurityAltIDSource = 606,

		/// <summary>FIX LegProduct tag (tag 607).</summary>
		FIX_LegProduct = 607,

		/// <summary>FIX LegCFICode tag (tag 608).</summary>
		FIX_LegCFICode = 608,

		/// <summary>FIX LegSecurityType tag (tag 609).</summary>
		FIX_LegSecurityType = 609,

		/// <summary>FIX LegMaturityMonthYear tag (tag 610).</summary>
		FIX_LegMaturityMonthYear = 610,

		/// <summary>FIX LegMaturityDate tag (tag 611).</summary>
		FIX_LegMaturityDate = 611,

		/// <summary>FIX LegStrikePrice tag (tag 612).</summary>
		FIX_LegStrikePrice = 612,

		/// <summary>FIX LegOptAttribute tag (tag 613).</summary>
		FIX_LegOptAttribute = 613,

		/// <summary>FIX LegContractMultiplier tag (tag 614).</summary>
		FIX_LegContractMultiplier = 614,

		/// <summary>FIX LegCouponRate tag (tag 615).</summary>
		FIX_LegCouponRate = 615,

		/// <summary>FIX LegSecurityExchange tag (tag 616).</summary>
		FIX_LegSecurityExchange = 616,

		/// <summary>FIX LegIssuer tag (tag 617).</summary>
		FIX_LegIssuer = 617,

		/// <summary>FIX EncodedLegIssuerLen tag (tag 618).</summary>
		FIX_EncodedLegIssuerLen = 618,

		/// <summary>FIX EncodedLegIssuer tag (tag 619).</summary>
		FIX_EncodedLegIssuer = 619,

		/// <summary>FIX LegSecurityDesc tag (tag 620).</summary>
		FIX_LegSecurityDesc = 620,

		/// <summary>FIX EncodedLegSecurityDescLen tag (tag 621).</summary>
		FIX_EncodedLegSecurityDescLen = 621,

		/// <summary>FIX EncodedLegSecurityDesc tag (tag 622).</summary>
		FIX_EncodedLegSecurityDesc = 622,

		/// <summary>FIX LegRatioQty tag (tag 623).</summary>
		FIX_LegRatioQty = 623,

		/// <summary>FIX LegSide tag (tag 624).</summary>
		FIX_LegSide = 624,

		/// <summary>FIX TradingSessionSubID tag (tag 625).</summary>
		FIX_TradingSessionSubID = 625,

		/// <summary>FIX AllocType tag (tag 626).</summary>
		FIX_AllocType = 626,

		/// <summary>FIX NoHops tag (tag 627).</summary>
		FIX_NoHops = 627,

		/// <summary>FIX HopCompID tag (tag 628).</summary>
		FIX_HopCompID = 628,

		/// <summary>FIX HopSendingTime tag (tag 629).</summary>
		FIX_HopSendingTime = 629,

		/// <summary>FIX HopRefID tag (tag 630).</summary>
		FIX_HopRefID = 630,

		/// <summary>FIX MidPx tag (tag 631).</summary>
		FIX_MidPx = 631,

		/// <summary>FIX BidYield tag (tag 632).</summary>
		FIX_BidYield = 632,

		/// <summary>FIX MidYield tag (tag 633).</summary>
		FIX_MidYield = 633,

		/// <summary>FIX OfferYield tag (tag 634).</summary>
		FIX_OfferYield = 634,

		/// <summary>FIX ClearingFeeIndicator tag (tag 635).</summary>
		FIX_ClearingFeeIndicator = 635,

		/// <summary>FIX WorkingIndicator tag (tag 636).</summary>
		FIX_WorkingIndicator = 636,

		/// <summary>FIX LegLastPx tag (tag 637).</summary>
		FIX_LegLastPx = 637,

		/// <summary>FIX PriorityIndicator tag (tag 638).</summary>
		FIX_PriorityIndicator = 638,

		/// <summary>FIX PriceImprovement tag (tag 639).</summary>
		FIX_PriceImprovement = 639,

		/// <summary>FIX Price2 tag (tag 640).</summary>
		FIX_Price2 = 640,

		/// <summary>FIX LastForwardPoints2 tag (tag 641).</summary>
		FIX_LastForwardPoints2 = 641,

		/// <summary>FIX BidForwardPoints2 tag (tag 642).</summary>
		FIX_BidForwardPoints2 = 642,

		/// <summary>FIX OfferForwardPoints2 tag (tag 643).</summary>
		FIX_OfferForwardPoints2 = 643,

		/// <summary>FIX RFQReqID tag (tag 644).</summary>
		FIX_RFQReqID = 644,

		/// <summary>FIX MktBidPx tag (tag 645).</summary>
		FIX_MktBidPx = 645,

		/// <summary>FIX MktOfferPx tag (tag 646).</summary>
		FIX_MktOfferPx = 646,

		/// <summary>FIX MinBidSize tag (tag 647).</summary>
		FIX_MinBidSize = 647,

		/// <summary>FIX MinOfferSize tag (tag 648).</summary>
		FIX_MinOfferSize = 648,

		/// <summary>FIX QuoteStatusReqID tag (tag 649).</summary>
		FIX_QuoteStatusReqID = 649,

		/// <summary>FIX LegalConfirm tag (tag 650).</summary>
		FIX_LegalConfirm = 650,

		/// <summary>FIX UnderlyingLastPx tag (tag 651).</summary>
		FIX_UnderlyingLastPx = 651,

		/// <summary>FIX UnderlyingLastQty tag (tag 652).</summary>
		FIX_UnderlyingLastQty = 652,

		/// <summary>FIX LegRefID tag (tag 654).</summary>
		FIX_LegRefID = 654,

		/// <summary>FIX ContraLegRefID tag (tag 655).</summary>
		FIX_ContraLegRefID = 655,

		/// <summary>FIX SettlCurrBidFxRate tag (tag 656).</summary>
		FIX_SettlCurrBidFxRate = 656,

		/// <summary>FIX SettlCurrOfferFxRate tag (tag 657).</summary>
		FIX_SettlCurrOfferFxRate = 657,

		/// <summary>FIX QuoteRequestRejectReason tag (tag 658).</summary>
		FIX_QuoteRequestRejectReason = 658,

		/// <summary>FIX SideComplianceID tag (tag 659).</summary>
		FIX_SideComplianceID = 659,

		/// <summary>FIX AcctIDSource tag (tag 660).</summary>
		FIX_AcctIDSource = 660,

		/// <summary>FIX AllocAcctIDSource tag (tag 661).</summary>
		FIX_AllocAcctIDSource = 661,

		/// <summary>FIX BenchmarkPrice tag (tag 662).</summary>
		FIX_BenchmarkPrice = 662,

		/// <summary>FIX BenchmarkPriceType tag (tag 663).</summary>
		FIX_BenchmarkPriceType = 663,

		/// <summary>FIX ConfirmID tag (tag 664).</summary>
		FIX_ConfirmID = 664,

		/// <summary>FIX ConfirmStatus tag (tag 665).</summary>
		FIX_ConfirmStatus = 665,

		/// <summary>FIX ConfirmTransType tag (tag 666).</summary>
		FIX_ConfirmTransType = 666,

		/// <summary>FIX ContractSettlMonth tag (tag 667).</summary>
		FIX_ContractSettlMonth = 667,

		/// <summary>FIX DeliveryForm tag (tag 668).</summary>
		FIX_DeliveryForm = 668,

		/// <summary>FIX LastParPx tag (tag 669).</summary>
		FIX_LastParPx = 669,

		/// <summary>FIX NoLegAllocs tag (tag 670).</summary>
		FIX_NoLegAllocs = 670,

		/// <summary>FIX LegAllocAccount tag (tag 671).</summary>
		FIX_LegAllocAccount = 671,

		/// <summary>FIX LegIndividualAllocID tag (tag 672).</summary>
		FIX_LegIndividualAllocID = 672,

		/// <summary>FIX LegAllocQty tag (tag 673).</summary>
		FIX_LegAllocQty = 673,

		/// <summary>FIX LegAllocAcctIDSource tag (tag 674).</summary>
		FIX_LegAllocAcctIDSource = 674,

		/// <summary>FIX LegSettlCurrency tag (tag 675).</summary>
		FIX_LegSettlCurrency = 675,

		/// <summary>FIX LegBenchmarkCurveCurrency tag (tag 676).</summary>
		FIX_LegBenchmarkCurveCurrency = 676,

		/// <summary>FIX LegBenchmarkCurveName tag (tag 677).</summary>
		FIX_LegBenchmarkCurveName = 677,

		/// <summary>FIX LegBenchmarkCurvePoint tag (tag 678).</summary>
		FIX_LegBenchmarkCurvePoint = 678,

		/// <summary>FIX LegBenchmarkPrice tag (tag 679).</summary>
		FIX_LegBenchmarkPrice = 679,

		/// <summary>FIX LegBenchmarkPriceType tag (tag 680).</summary>
		FIX_LegBenchmarkPriceType = 680,

		/// <summary>FIX LegBidPx tag (tag 681).</summary>
		FIX_LegBidPx = 681,

		/// <summary>FIX LegIOIQty tag (tag 682).</summary>
		FIX_LegIOIQty = 682,

		/// <summary>FIX NoLegStipulations tag (tag 683).</summary>
		FIX_NoLegStipulations = 683,

		/// <summary>FIX LegOfferPx tag (tag 684).</summary>
		FIX_LegOfferPx = 684,

		/// <summary>FIX LegOrderQty tag (tag 685).</summary>
		FIX_LegOrderQty = 685,

		/// <summary>FIX LegPriceType tag (tag 686).</summary>
		FIX_LegPriceType = 686,

		/// <summary>FIX LegQty tag (tag 687).</summary>
		FIX_LegQty = 687,

		/// <summary>FIX LegStipulationType tag (tag 688).</summary>
		FIX_LegStipulationType = 688,

		/// <summary>FIX LegStipulationValue tag (tag 689).</summary>
		FIX_LegStipulationValue = 689,

		/// <summary>FIX LegSwapType tag (tag 690).</summary>
		FIX_LegSwapType = 690,

		/// <summary>FIX Pool tag (tag 691).</summary>
		FIX_Pool = 691,

		/// <summary>FIX QuotePriceType tag (tag 692).</summary>
		FIX_QuotePriceType = 692,

		/// <summary>FIX QuoteRespID tag (tag 693).</summary>
		FIX_QuoteRespID = 693,

		/// <summary>FIX QuoteRespType tag (tag 694).</summary>
		FIX_QuoteRespType = 694,

		/// <summary>FIX QuoteQualifier tag (tag 695).</summary>
		FIX_QuoteQualifier = 695,

		/// <summary>FIX YieldRedemptionDate tag (tag 696).</summary>
		FIX_YieldRedemptionDate = 696,

		/// <summary>FIX YieldRedemptionPrice tag (tag 697).</summary>
		FIX_YieldRedemptionPrice = 697,

		/// <summary>FIX YieldRedemptionPriceType tag (tag 698).</summary>
		FIX_YieldRedemptionPriceType = 698,

		/// <summary>FIX BenchmarkSecurityID tag (tag 699).</summary>
		FIX_BenchmarkSecurityID = 699,

		/// <summary>FIX ReversalIndicator tag (tag 700).</summary>
		FIX_ReversalIndicator = 700,

		/// <summary>FIX YieldCalcDate tag (tag 701).</summary>
		FIX_YieldCalcDate = 701,

		/// <summary>FIX NoPositions tag (tag 702).</summary>
		FIX_NoPositions = 702,

		/// <summary>FIX PosType tag (tag 703).</summary>
		FIX_PosType = 703,

		/// <summary>FIX LongQty tag (tag 704).</summary>
		FIX_LongQty = 704,

		/// <summary>FIX ShortQty tag (tag 705).</summary>
		FIX_ShortQty = 705,

		/// <summary>FIX PosQtyStatus tag (tag 706).</summary>
		FIX_PosQtyStatus = 706,

		/// <summary>FIX PosAmtType tag (tag 707).</summary>
		FIX_PosAmtType = 707,

		/// <summary>FIX PosAmt tag (tag 708).</summary>
		FIX_PosAmt = 708,

		/// <summary>FIX PosTransType tag (tag 709).</summary>
		FIX_PosTransType = 709,

		/// <summary>FIX PosReqID tag (tag 710).</summary>
		FIX_PosReqID = 710,

		/// <summary>FIX NoUnderlyings tag (tag 711).</summary>
		FIX_NoUnderlyings = 711,

		/// <summary>FIX PosMaintAction tag (tag 712).</summary>
		FIX_PosMaintAction = 712,

		/// <summary>FIX OrigPosReqRefID tag (tag 713).</summary>
		FIX_OrigPosReqRefID = 713,

		/// <summary>FIX PosMaintRptRefID tag (tag 714).</summary>
		FIX_PosMaintRptRefID = 714,

		/// <summary>FIX ClearingBusinessDate tag (tag 715).</summary>
		FIX_ClearingBusinessDate = 715,

		/// <summary>FIX SettlSessID tag (tag 716).</summary>
		FIX_SettlSessID = 716,

		/// <summary>FIX SettlSessSubID tag (tag 717).</summary>
		FIX_SettlSessSubID = 717,

		/// <summary>FIX AdjustmentType tag (tag 718).</summary>
		FIX_AdjustmentType = 718,

		/// <summary>FIX ContraryInstructionIndicator tag (tag 719).</summary>
		FIX_ContraryInstructionIndicator = 719,

		/// <summary>FIX PriorSpreadIndicator tag (tag 720).</summary>
		FIX_PriorSpreadIndicator = 720,

		/// <summary>FIX PosMaintRptID tag (tag 721).</summary>
		FIX_PosMaintRptID = 721,

		/// <summary>FIX PosMaintStatus tag (tag 722).</summary>
		FIX_PosMaintStatus = 722,

		/// <summary>FIX PosMaintResult tag (tag 723).</summary>
		FIX_PosMaintResult = 723,

		/// <summary>FIX PosReqType tag (tag 724).</summary>
		FIX_PosReqType = 724,

		/// <summary>FIX ResponseTransportType tag (tag 725).</summary>
		FIX_ResponseTransportType = 725,

		/// <summary>FIX ResponseDestination tag (tag 726).</summary>
		FIX_ResponseDestination = 726,

		/// <summary>FIX TotalNumPosReports tag (tag 727).</summary>
		FIX_TotalNumPosReports = 727,

		/// <summary>FIX PosReqResult tag (tag 728).</summary>
		FIX_PosReqResult = 728,

		/// <summary>FIX PosReqStatus tag (tag 729).</summary>
		FIX_PosReqStatus = 729,

		/// <summary>FIX SettlPrice tag (tag 730).</summary>
		FIX_SettlPrice = 730,

		/// <summary>FIX SettlPriceType tag (tag 731).</summary>
		FIX_SettlPriceType = 731,

		/// <summary>FIX UnderlyingSettlPrice tag (tag 732).</summary>
		FIX_UnderlyingSettlPrice = 732,

		/// <summary>FIX UnderlyingSettlPriceType tag (tag 733).</summary>
		FIX_UnderlyingSettlPriceType = 733,

		/// <summary>FIX PriorSettlPrice tag (tag 734).</summary>
		FIX_PriorSettlPrice = 734,

		/// <summary>FIX NoQuoteQualifiers tag (tag 735).</summary>
		FIX_NoQuoteQualifiers = 735,

		/// <summary>FIX AllocSettlCurrency tag (tag 736).</summary>
		FIX_AllocSettlCurrency = 736,

		/// <summary>FIX AllocSettlCurrAmt tag (tag 737).</summary>
		FIX_AllocSettlCurrAmt = 737,

		/// <summary>FIX InterestAtMaturity tag (tag 738).</summary>
		FIX_InterestAtMaturity = 738,

		/// <summary>FIX LegDatedDate tag (tag 739).</summary>
		FIX_LegDatedDate = 739,

		/// <summary>FIX LegPool tag (tag 740).</summary>
		FIX_LegPool = 740,

		/// <summary>FIX AllocInterestAtMaturity tag (tag 741).</summary>
		FIX_AllocInterestAtMaturity = 741,

		/// <summary>FIX AllocAccruedInterestAmt tag (tag 742).</summary>
		FIX_AllocAccruedInterestAmt = 742,

		/// <summary>FIX DeliveryDate tag (tag 743).</summary>
		FIX_DeliveryDate = 743,

		/// <summary>FIX AssignmentMethod tag (tag 744).</summary>
		FIX_AssignmentMethod = 744,

		/// <summary>FIX AssignmentUnit tag (tag 745).</summary>
		FIX_AssignmentUnit = 745,

		/// <summary>FIX OpenInterest tag (tag 746).</summary>
		FIX_OpenInterest = 746,

		/// <summary>FIX ExerciseMethod tag (tag 747).</summary>
		FIX_ExerciseMethod = 747,

		/// <summary>FIX TotNumTradeReports tag (tag 748).</summary>
		FIX_TotNumTradeReports = 748,

		/// <summary>FIX TradeRequestResult tag (tag 749).</summary>
		FIX_TradeRequestResult = 749,

		/// <summary>FIX TradeRequestStatus tag (tag 750).</summary>
		FIX_TradeRequestStatus = 750,

		/// <summary>FIX TradeReportRejectReason tag (tag 751).</summary>
		FIX_TradeReportRejectReason = 751,

		/// <summary>FIX SideMultiLegReportingType tag (tag 752).</summary>
		FIX_SideMultiLegReportingType = 752,

		/// <summary>FIX NoPosAmt tag (tag 753).</summary>
		FIX_NoPosAmt = 753,

		/// <summary>FIX AutoAcceptIndicator tag (tag 754).</summary>
		FIX_AutoAcceptIndicator = 754,

		/// <summary>FIX AllocReportID tag (tag 755).</summary>
		FIX_AllocReportID = 755,

		/// <summary>FIX NoNested2PartyIDs tag (tag 756).</summary>
		FIX_NoNested2PartyIDs = 756,

		/// <summary>FIX Nested2PartyID tag (tag 757).</summary>
		FIX_Nested2PartyID = 757,

		/// <summary>FIX Nested2PartyIDSource tag (tag 758).</summary>
		FIX_Nested2PartyIDSource = 758,

		/// <summary>FIX Nested2PartyRole tag (tag 759).</summary>
		FIX_Nested2PartyRole = 759,

		/// <summary>FIX Nested2PartySubID tag (tag 760).</summary>
		FIX_Nested2PartySubID = 760,

		/// <summary>FIX BenchmarkSecurityIDSource tag (tag 761).</summary>
		FIX_BenchmarkSecurityIDSource = 761,

		/// <summary>FIX SecuritySubType tag (tag 762).</summary>
		FIX_SecuritySubType = 762,

		/// <summary>FIX UnderlyingSecuritySubType tag (tag 763).</summary>
		FIX_UnderlyingSecuritySubType = 763,

		/// <summary>FIX LegSecuritySubType tag (tag 764).</summary>
		FIX_LegSecuritySubType = 764,

		/// <summary>FIX AllowableOneSidednessPct tag (tag 765).</summary>
		FIX_AllowableOneSidednessPct = 765,

		/// <summary>FIX AllowableOneSidednessValue tag (tag 766).</summary>
		FIX_AllowableOneSidednessValue = 766,

		/// <summary>FIX AllowableOneSidednessCurr tag (tag 767).</summary>
		FIX_AllowableOneSidednessCurr = 767,

		/// <summary>FIX NoTrdRegTimestamps tag (tag 768).</summary>
		FIX_NoTrdRegTimestamps = 768,

		/// <summary>FIX TrdRegTimestamp tag (tag 769).</summary>
		FIX_TrdRegTimestamp = 769,

		/// <summary>FIX TrdRegTimestampType tag (tag 770).</summary>
		FIX_TrdRegTimestampType = 770,

		/// <summary>FIX TrdRegTimestampOrigin tag (tag 771).</summary>
		FIX_TrdRegTimestampOrigin = 771,

		/// <summary>FIX ConfirmRefID tag (tag 772).</summary>
		FIX_ConfirmRefID = 772,

		/// <summary>FIX ConfirmType tag (tag 773).</summary>
		FIX_ConfirmType = 773,

		/// <summary>FIX ConfirmRejReason tag (tag 774).</summary>
		FIX_ConfirmRejReason = 774,

		/// <summary>FIX BookingType tag (tag 775).</summary>
		FIX_BookingType = 775,

		/// <summary>FIX IndividualAllocRejCode tag (tag 776).</summary>
		FIX_IndividualAllocRejCode = 776,

		/// <summary>FIX SettlInstMsgID tag (tag 777).</summary>
		FIX_SettlInstMsgID = 777,

		/// <summary>FIX NoSettlInst tag (tag 778).</summary>
		FIX_NoSettlInst = 778,

		/// <summary>FIX LastUpdateTime tag (tag 779).</summary>
		FIX_LastUpdateTime = 779,

		/// <summary>FIX AllocSettlInstType tag (tag 780).</summary>
		FIX_AllocSettlInstType = 780,

		/// <summary>FIX NoSettlPartyIDs tag (tag 781).</summary>
		FIX_NoSettlPartyIDs = 781,

		/// <summary>FIX SettlPartyID tag (tag 782).</summary>
		FIX_SettlPartyID = 782,

		/// <summary>FIX SettlPartyIDSource tag (tag 783).</summary>
		FIX_SettlPartyIDSource = 783,

		/// <summary>FIX SettlPartyRole tag (tag 784).</summary>
		FIX_SettlPartyRole = 784,

		/// <summary>FIX SettlPartySubID tag (tag 785).</summary>
		FIX_SettlPartySubID = 785,

		/// <summary>FIX SettlPartySubIDType tag (tag 786).</summary>
		FIX_SettlPartySubIDType = 786,

		/// <summary>FIX DlvyInstType tag (tag 787).</summary>
		FIX_DlvyInstType = 787,

		/// <summary>FIX TerminationType tag (tag 788).</summary>
		FIX_TerminationType = 788,

		/// <summary>FIX NextExpectedMsgSeqNum tag (tag 789).</summary>
		FIX_NextExpectedMsgSeqNum = 789,

		/// <summary>FIX OrdStatusReqID tag (tag 790).</summary>
		FIX_OrdStatusReqID = 790,

		/// <summary>FIX SettlInstReqID tag (tag 791).</summary>
		FIX_SettlInstReqID = 791,

		/// <summary>FIX SettlInstReqRejCode tag (tag 792).</summary>
		FIX_SettlInstReqRejCode = 792,

		/// <summary>FIX SecondaryAllocID tag (tag 793).</summary>
		FIX_SecondaryAllocID = 793,

		/// <summary>FIX AllocReportType tag (tag 794).</summary>
		FIX_AllocReportType = 794,

		/// <summary>FIX AllocReportRefID tag (tag 795).</summary>
		FIX_AllocReportRefID = 795,

		/// <summary>FIX AllocCancReplaceReason tag (tag 796).</summary>
		FIX_AllocCancReplaceReason = 796,

		/// <summary>FIX CopyMsgIndicator tag (tag 797).</summary>
		FIX_CopyMsgIndicator = 797,

		/// <summary>FIX AllocAccountType tag (tag 798).</summary>
		FIX_AllocAccountType = 798,

		/// <summary>FIX OrderAvgPx tag (tag 799).</summary>
		FIX_OrderAvgPx = 799,

		/// <summary>FIX OrderBookingQty tag (tag 800).</summary>
		FIX_OrderBookingQty = 800,

		/// <summary>FIX NoSettlPartySubIDs tag (tag 801).</summary>
		FIX_NoSettlPartySubIDs = 801,

		/// <summary>FIX NoPartySubIDs tag (tag 802).</summary>
		FIX_NoPartySubIDs = 802,

		/// <summary>FIX PartySubIDType tag (tag 803).</summary>
		FIX_PartySubIDType = 803,

		/// <summary>FIX NoNestedPartySubIDs tag (tag 804).</summary>
		FIX_NoNestedPartySubIDs = 804,

		/// <summary>FIX NestedPartySubIDType tag (tag 805).</summary>
		FIX_NestedPartySubIDType = 805,

		/// <summary>FIX NoNested2PartySubIDs tag (tag 806).</summary>
		FIX_NoNested2PartySubIDs = 806,

		/// <summary>FIX Nested2PartySubIDType tag (tag 807).</summary>
		FIX_Nested2PartySubIDType = 807,

		/// <summary>FIX AllocIntermedReqType tag (tag 808).</summary>
		FIX_AllocIntermedReqType = 808,

		/// <summary>FIX UnderlyingPx tag (tag 810).</summary>
		FIX_UnderlyingPx = 810,

		/// <summary>FIX PriceDelta tag (tag 811).</summary>
		FIX_PriceDelta = 811,

		/// <summary>FIX ApplQueueMax tag (tag 812).</summary>
		FIX_ApplQueueMax = 812,

		/// <summary>FIX ApplQueueDepth tag (tag 813).</summary>
		FIX_ApplQueueDepth = 813,

		/// <summary>FIX ApplQueueResolution tag (tag 814).</summary>
		FIX_ApplQueueResolution = 814,

		/// <summary>FIX ApplQueueAction tag (tag 815).</summary>
		FIX_ApplQueueAction = 815,

		/// <summary>FIX NoAltMDSource tag (tag 816).</summary>
		FIX_NoAltMDSource = 816,

		/// <summary>FIX AltMDSourceID tag (tag 817).</summary>
		FIX_AltMDSourceID = 817,

		/// <summary>FIX SecondaryTradeReportID tag (tag 818).</summary>
		FIX_SecondaryTradeReportID = 818,

		/// <summary>FIX AvgPxIndicator tag (tag 819).</summary>
		FIX_AvgPxIndicator = 819,

		/// <summary>FIX TradeLinkID tag (tag 820).</summary>
		FIX_TradeLinkID = 820,

		/// <summary>FIX OrderInputDevice tag (tag 821).</summary>
		FIX_OrderInputDevice = 821,

		/// <summary>FIX UnderlyingTradingSessionID tag (tag 822).</summary>
		FIX_UnderlyingTradingSessionID = 822,

		/// <summary>FIX UnderlyingTradingSessionSubID tag (tag 823).</summary>
		FIX_UnderlyingTradingSessionSubID = 823,

		/// <summary>FIX TradeLegRefID tag (tag 824).</summary>
		FIX_TradeLegRefID = 824,

		/// <summary>FIX ExchangeRule tag (tag 825).</summary>
		FIX_ExchangeRule = 825,

		/// <summary>FIX TradeAllocIndicator tag (tag 826).</summary>
		FIX_TradeAllocIndicator = 826,

		/// <summary>FIX ExpirationCycle tag (tag 827).</summary>
		FIX_ExpirationCycle = 827,

		/// <summary>FIX TrdType tag (tag 828).</summary>
		FIX_TrdType = 828,

		/// <summary>FIX TrdSubType tag (tag 829).</summary>
		FIX_TrdSubType = 829,

		/// <summary>FIX TransferReason tag (tag 830).</summary>
		FIX_TransferReason = 830,

		/// <summary>FIX TotNumAssignmentReports tag (tag 832).</summary>
		FIX_TotNumAssignmentReports = 832,

		/// <summary>FIX AsgnRptID tag (tag 833).</summary>
		FIX_AsgnRptID = 833,

		/// <summary>FIX ThresholdAmount tag (tag 834).</summary>
		FIX_ThresholdAmount = 834,

		/// <summary>FIX PegMoveType tag (tag 835).</summary>
		FIX_PegMoveType = 835,

		/// <summary>FIX PegOffsetType tag (tag 836).</summary>
		FIX_PegOffsetType = 836,

		/// <summary>FIX PegLimitType tag (tag 837).</summary>
		FIX_PegLimitType = 837,

		/// <summary>FIX PegRoundDirection tag (tag 838).</summary>
		FIX_PegRoundDirection = 838,

		/// <summary>FIX PeggedPrice tag (tag 839).</summary>
		FIX_PeggedPrice = 839,

		/// <summary>FIX PegScope tag (tag 840).</summary>
		FIX_PegScope = 840,

		/// <summary>FIX DiscretionMoveType tag (tag 841).</summary>
		FIX_DiscretionMoveType = 841,

		/// <summary>FIX DiscretionOffsetType tag (tag 842).</summary>
		FIX_DiscretionOffsetType = 842,

		/// <summary>FIX DiscretionLimitType tag (tag 843).</summary>
		FIX_DiscretionLimitType = 843,

		/// <summary>FIX DiscretionRoundDirection tag (tag 844).</summary>
		FIX_DiscretionRoundDirection = 844,

		/// <summary>FIX DiscretionPrice tag (tag 845).</summary>
		FIX_DiscretionPrice = 845,

		/// <summary>FIX DiscretionScope tag (tag 846).</summary>
		FIX_DiscretionScope = 846,

		/// <summary>FIX TargetStrategy tag (tag 847).</summary>
		FIX_TargetStrategy = 847,

		/// <summary>FIX TargetStrategyParameters tag (tag 848).</summary>
		FIX_TargetStrategyParameters = 848,

		/// <summary>FIX ParticipationRate tag (tag 849).</summary>
		FIX_ParticipationRate = 849,

		/// <summary>FIX TargetStrategyPerformance tag (tag 850).</summary>
		FIX_TargetStrategyPerformance = 850,

		/// <summary>FIX LastLiquidityInd tag (tag 851).</summary>
		FIX_LastLiquidityInd = 851,

		/// <summary>FIX PublishTrdIndicator tag (tag 852).</summary>
		FIX_PublishTrdIndicator = 852,

		/// <summary>FIX ShortSaleReason tag (tag 853).</summary>
		FIX_ShortSaleReason = 853,

		/// <summary>FIX QtyType tag (tag 854).</summary>
		FIX_QtyType = 854,

		/// <summary>FIX SecondaryTrdType tag (tag 855).</summary>
		FIX_SecondaryTrdType = 855,

		/// <summary>FIX TradeReportType tag (tag 856).</summary>
		FIX_TradeReportType = 856,

		/// <summary>FIX AllocNoOrdersType tag (tag 857).</summary>
		FIX_AllocNoOrdersType = 857,

		/// <summary>FIX SharedCommission tag (tag 858).</summary>
		FIX_SharedCommission = 858,

		/// <summary>FIX ConfirmReqID tag (tag 859).</summary>
		FIX_ConfirmReqID = 859,

		/// <summary>FIX AvgParPx tag (tag 860).</summary>
		FIX_AvgParPx = 860,

		/// <summary>FIX ReportedPx tag (tag 861).</summary>
		FIX_ReportedPx = 861,

		/// <summary>FIX NoCapacities tag (tag 862).</summary>
		FIX_NoCapacities = 862,

		/// <summary>FIX OrderCapacityQty tag (tag 863).</summary>
		FIX_OrderCapacityQty = 863,

		/// <summary>FIX NoEvents tag (tag 864).</summary>
		FIX_NoEvents = 864,

		/// <summary>FIX EventType tag (tag 865).</summary>
		FIX_EventType = 865,

		/// <summary>FIX EventDate tag (tag 866).</summary>
		FIX_EventDate = 866,

		/// <summary>FIX EventPx tag (tag 867).</summary>
		FIX_EventPx = 867,

		/// <summary>FIX EventText tag (tag 868).</summary>
		FIX_EventText = 868,

		/// <summary>FIX PctAtRisk tag (tag 869).</summary>
		FIX_PctAtRisk = 869,

		/// <summary>FIX NoInstrAttrib tag (tag 870).</summary>
		FIX_NoInstrAttrib = 870,

		/// <summary>FIX InstrAttribType tag (tag 871).</summary>
		FIX_InstrAttribType = 871,

		/// <summary>FIX InstrAttribValue tag (tag 872).</summary>
		FIX_InstrAttribValue = 872,

		/// <summary>FIX DatedDate tag (tag 873).</summary>
		FIX_DatedDate = 873,

		/// <summary>FIX InterestAccrualDate tag (tag 874).</summary>
		FIX_InterestAccrualDate = 874,

		/// <summary>FIX CPProgram tag (tag 875).</summary>
		FIX_CPProgram = 875,

		/// <summary>FIX CPRegType tag (tag 876).</summary>
		FIX_CPRegType = 876,

		/// <summary>FIX UnderlyingCPProgram tag (tag 877).</summary>
		FIX_UnderlyingCPProgram = 877,

		/// <summary>FIX UnderlyingCPRegType tag (tag 878).</summary>
		FIX_UnderlyingCPRegType = 878,

		/// <summary>FIX UnderlyingQty tag (tag 879).</summary>
		FIX_UnderlyingQty = 879,

		/// <summary>FIX TrdMatchID tag (tag 880).</summary>
		FIX_TrdMatchID = 880,

		/// <summary>FIX SecondaryTradeReportRefID tag (tag 881).</summary>
		FIX_SecondaryTradeReportRefID = 881,

		/// <summary>FIX UnderlyingDirtyPrice tag (tag 882).</summary>
		FIX_UnderlyingDirtyPrice = 882,

		/// <summary>FIX UnderlyingEndPrice tag (tag 883).</summary>
		FIX_UnderlyingEndPrice = 883,

		/// <summary>FIX UnderlyingStartValue tag (tag 884).</summary>
		FIX_UnderlyingStartValue = 884,

		/// <summary>FIX UnderlyingCurrentValue tag (tag 885).</summary>
		FIX_UnderlyingCurrentValue = 885,

		/// <summary>FIX UnderlyingEndValue tag (tag 886).</summary>
		FIX_UnderlyingEndValue = 886,

		/// <summary>FIX NoUnderlyingStips tag (tag 887).</summary>
		FIX_NoUnderlyingStips = 887,

		/// <summary>FIX UnderlyingStipType tag (tag 888).</summary>
		FIX_UnderlyingStipType = 888,

		/// <summary>FIX UnderlyingStipValue tag (tag 889).</summary>
		FIX_UnderlyingStipValue = 889,

		/// <summary>FIX MaturityNetMoney tag (tag 890).</summary>
		FIX_MaturityNetMoney = 890,

		/// <summary>FIX MiscFeeBasis tag (tag 891).</summary>
		FIX_MiscFeeBasis = 891,

		/// <summary>FIX TotNoAllocs tag (tag 892).</summary>
		FIX_TotNoAllocs = 892,

		/// <summary>FIX LastFragment tag (tag 893).</summary>
		FIX_LastFragment = 893,

		/// <summary>FIX CollReqID tag (tag 894).</summary>
		FIX_CollReqID = 894,

		/// <summary>FIX CollAsgnReason tag (tag 895).</summary>
		FIX_CollAsgnReason = 895,

		/// <summary>FIX CollInquiryQualifier tag (tag 896).</summary>
		FIX_CollInquiryQualifier = 896,

		/// <summary>FIX NoTrades tag (tag 897).</summary>
		FIX_NoTrades = 897,

		/// <summary>FIX MarginRatio tag (tag 898).</summary>
		FIX_MarginRatio = 898,

		/// <summary>FIX MarginExcess tag (tag 899).</summary>
		FIX_MarginExcess = 899,

		/// <summary>FIX TotalNetValue tag (tag 900).</summary>
		FIX_TotalNetValue = 900,

		/// <summary>FIX CashOutstanding tag (tag 901).</summary>
		FIX_CashOutstanding = 901,

		/// <summary>FIX CollAsgnID tag (tag 902).</summary>
		FIX_CollAsgnID = 902,

		/// <summary>FIX CollAsgnTransType tag (tag 903).</summary>
		FIX_CollAsgnTransType = 903,

		/// <summary>FIX CollRespID tag (tag 904).</summary>
		FIX_CollRespID = 904,

		/// <summary>FIX CollAsgnRespType tag (tag 905).</summary>
		FIX_CollAsgnRespType = 905,

		/// <summary>FIX CollAsgnRejectReason tag (tag 906).</summary>
		FIX_CollAsgnRejectReason = 906,

		/// <summary>FIX CollAsgnRefID tag (tag 907).</summary>
		FIX_CollAsgnRefID = 907,

		/// <summary>FIX CollRptID tag (tag 908).</summary>
		FIX_CollRptID = 908,

		/// <summary>FIX CollInquiryID tag (tag 909).</summary>
		FIX_CollInquiryID = 909,

		/// <summary>FIX CollStatus tag (tag 910).</summary>
		FIX_CollStatus = 910,

		/// <summary>FIX TotNumReports tag (tag 911).</summary>
		FIX_TotNumReports = 911,

		/// <summary>FIX LastRptRequested tag (tag 912).</summary>
		FIX_LastRptRequested = 912,

		/// <summary>FIX AgreementDesc tag (tag 913).</summary>
		FIX_AgreementDesc = 913,

		/// <summary>FIX AgreementID tag (tag 914).</summary>
		FIX_AgreementID = 914,

		/// <summary>FIX AgreementDate tag (tag 915).</summary>
		FIX_AgreementDate = 915,

		/// <summary>FIX StartDate tag (tag 916).</summary>
		FIX_StartDate = 916,

		/// <summary>FIX EndDate tag (tag 917).</summary>
		FIX_EndDate = 917,

		/// <summary>FIX AgreementCurrency tag (tag 918).</summary>
		FIX_AgreementCurrency = 918,

		/// <summary>FIX DeliveryType tag (tag 919).</summary>
		FIX_DeliveryType = 919,

		/// <summary>FIX EndAccruedInterestAmt tag (tag 920).</summary>
		FIX_EndAccruedInterestAmt = 920,

		/// <summary>FIX StartCash tag (tag 921).</summary>
		FIX_StartCash = 921,

		/// <summary>FIX EndCash tag (tag 922).</summary>
		FIX_EndCash = 922,

		/// <summary>FIX UserRequestID tag (tag 923).</summary>
		FIX_UserRequestID = 923,

		/// <summary>FIX UserRequestType tag (tag 924).</summary>
		FIX_UserRequestType = 924,

		/// <summary>FIX NewPassword tag (tag 925).</summary>
		FIX_NewPassword = 925,

		/// <summary>FIX UserStatus tag (tag 926).</summary>
		FIX_UserStatus = 926,

		/// <summary>FIX UserStatusText tag (tag 927).</summary>
		FIX_UserStatusText = 927,

		/// <summary>FIX StatusValue tag (tag 928).</summary>
		FIX_StatusValue = 928,

		/// <summary>FIX StatusText tag (tag 929).</summary>
		FIX_StatusText = 929,

		/// <summary>FIX RefCompID tag (tag 930).</summary>
		FIX_RefCompID = 930,

		/// <summary>FIX RefSubID tag (tag 931).</summary>
		FIX_RefSubID = 931,

		/// <summary>FIX NetworkResponseID tag (tag 932).</summary>
		FIX_NetworkResponseID = 932,

		/// <summary>FIX NetworkRequestID tag (tag 933).</summary>
		FIX_NetworkRequestID = 933,

		/// <summary>FIX LastNetworkResponseID tag (tag 934).</summary>
		FIX_LastNetworkResponseID = 934,

		/// <summary>FIX NetworkRequestType tag (tag 935).</summary>
		FIX_NetworkRequestType = 935,

		/// <summary>FIX NoCompIDs tag (tag 936).</summary>
		FIX_NoCompIDs = 936,

		/// <summary>FIX NetworkStatusResponseType tag (tag 937).</summary>
		FIX_NetworkStatusResponseType = 937,

		/// <summary>FIX NoCollInquiryQualifier tag (tag 938).</summary>
		FIX_NoCollInquiryQualifier = 938,

		/// <summary>FIX TrdRptStatus tag (tag 939).</summary>
		FIX_TrdRptStatus = 939,

		/// <summary>FIX AffirmStatus tag (tag 940).</summary>
		FIX_AffirmStatus = 940,

		/// <summary>FIX UnderlyingStrikeCurrency tag (tag 941).</summary>
		FIX_UnderlyingStrikeCurrency = 941,

		/// <summary>FIX LegStrikeCurrency tag (tag 942).</summary>
		FIX_LegStrikeCurrency = 942,

		/// <summary>FIX TimeBracket tag (tag 943).</summary>
		FIX_TimeBracket = 943,

		/// <summary>FIX CollAction tag (tag 944).</summary>
		FIX_CollAction = 944,

		/// <summary>FIX CollInquiryStatus tag (tag 945).</summary>
		FIX_CollInquiryStatus = 945,

		/// <summary>FIX CollInquiryResult tag (tag 946).</summary>
		FIX_CollInquiryResult = 946,

		/// <summary>FIX StrikeCurrency tag (tag 947).</summary>
		FIX_StrikeCurrency = 947,

		/// <summary>FIX NoNested3PartyIDs tag (tag 948).</summary>
		FIX_NoNested3PartyIDs = 948,

		/// <summary>FIX Nested3PartyID tag (tag 949).</summary>
		FIX_Nested3PartyID = 949,

		/// <summary>FIX Nested3PartyIDSource tag (tag 950).</summary>
		FIX_Nested3PartyIDSource = 950,

		/// <summary>FIX Nested3PartyRole tag (tag 951).</summary>
		FIX_Nested3PartyRole = 951,

		/// <summary>FIX NoNested3PartySubIDs tag (tag 952).</summary>
		FIX_NoNested3PartySubIDs = 952,

		/// <summary>FIX Nested3PartySubID tag (tag 953).</summary>
		FIX_Nested3PartySubID = 953,

		/// <summary>FIX Nested3PartySubIDType tag (tag 954).</summary>
		FIX_Nested3PartySubIDType = 954,

		/// <summary>FIX LegContractSettlMonth tag (tag 955).</summary>
		FIX_LegContractSettlMonth = 955,

		/// <summary>FIX LegInterestAccrualDate tag (tag 956).</summary>
		FIX_LegInterestAccrualDate = 956,

		/// <summary>FIX NoStrategyParameters tag (tag 957).</summary>
		FIX_NoStrategyParameters = 957,

		/// <summary>FIX StrategyParameterName tag (tag 958).</summary>
		FIX_StrategyParameterName = 958,

		/// <summary>FIX StrategyParameterType tag (tag 959).</summary>
		FIX_StrategyParameterType = 959,

		/// <summary>FIX StrategyParameterValue tag (tag 960).</summary>
		FIX_StrategyParameterValue = 960,

		/// <summary>FIX HostCrossID tag (tag 961).</summary>
		FIX_HostCrossID = 961,

		/// <summary>FIX SideTimeInForce tag (tag 962).</summary>
		FIX_SideTimeInForce = 962,

		/// <summary>FIX MDReportID tag (tag 963).</summary>
		FIX_MDReportID = 963,

		/// <summary>FIX SecurityReportID tag (tag 964).</summary>
		FIX_SecurityReportID = 964,

		/// <summary>FIX SecurityStatus tag (tag 965).</summary>
		FIX_SecurityStatus = 965,

		/// <summary>FIX SettleOnOpenFlag tag (tag 966).</summary>
		FIX_SettleOnOpenFlag = 966,

		/// <summary>FIX StrikeMultiplier tag (tag 967).</summary>
		FIX_StrikeMultiplier = 967,

		/// <summary>FIX StrikeValue tag (tag 968).</summary>
		FIX_StrikeValue = 968,

		/// <summary>FIX MinPriceIncrement tag (tag 969).</summary>
		FIX_MinPriceIncrement = 969,

		/// <summary>FIX PositionLimit tag (tag 970).</summary>
		FIX_PositionLimit = 970,

		/// <summary>FIX NTPositionLimit tag (tag 971).</summary>
		FIX_NTPositionLimit = 971,

		/// <summary>FIX UnderlyingAllocationPercent tag (tag 972).</summary>
		FIX_UnderlyingAllocationPercent = 972,

		/// <summary>FIX UnderlyingCashAmount tag (tag 973).</summary>
		FIX_UnderlyingCashAmount = 973,

		/// <summary>FIX UnderlyingCashType tag (tag 974).</summary>
		FIX_UnderlyingCashType = 974,

		/// <summary>FIX UnderlyingSettlementType tag (tag 975).</summary>
		FIX_UnderlyingSettlementType = 975,

		/// <summary>FIX QuantityDate tag (tag 976).</summary>
		FIX_QuantityDate = 976,

		/// <summary>FIX ContIntRptID tag (tag 977).</summary>
		FIX_ContIntRptID = 977,

		/// <summary>FIX LateIndicator tag (tag 978).</summary>
		FIX_LateIndicator = 978,

		/// <summary>FIX InputSource tag (tag 979).</summary>
		FIX_InputSource = 979,

		/// <summary>FIX SecurityUpdateAction tag (tag 980).</summary>
		FIX_SecurityUpdateAction = 980,

		/// <summary>FIX NoExpiration tag (tag 981).</summary>
		FIX_NoExpiration = 981,

		/// <summary>FIX ExpirationQtyType tag (tag 982).</summary>
		FIX_ExpirationQtyType = 982,

		/// <summary>FIX ExpQty tag (tag 983).</summary>
		FIX_ExpQty = 983,

		/// <summary>FIX NoUnderlyingAmounts tag (tag 984).</summary>
		FIX_NoUnderlyingAmounts = 984,

		/// <summary>FIX UnderlyingPayAmount tag (tag 985).</summary>
		FIX_UnderlyingPayAmount = 985,

		/// <summary>FIX UnderlyingCollectAmount tag (tag 986).</summary>
		FIX_UnderlyingCollectAmount = 986,

		/// <summary>FIX UnderlyingSettlementDate tag (tag 987).</summary>
		FIX_UnderlyingSettlementDate = 987,

		/// <summary>FIX UnderlyingSettlementStatus tag (tag 988).</summary>
		FIX_UnderlyingSettlementStatus = 988,

		/// <summary>FIX SecondaryIndividualAllocID tag (tag 989).</summary>
		FIX_SecondaryIndividualAllocID = 989,

		/// <summary>FIX LegReportID tag (tag 990).</summary>
		FIX_LegReportID = 990,

		/// <summary>FIX RndPx tag (tag 991).</summary>
		FIX_RndPx = 991,

		/// <summary>FIX IndividualAllocType tag (tag 992).</summary>
		FIX_IndividualAllocType = 992,

		/// <summary>FIX AllocCustomerCapacity tag (tag 993).</summary>
		FIX_AllocCustomerCapacity = 993,

		/// <summary>FIX TierCode tag (tag 994).</summary>
		FIX_TierCode = 994,

		/// <summary>FIX UnitOfMeasure tag (tag 996).</summary>
		FIX_UnitOfMeasure = 996,

		/// <summary>FIX TimeUnit tag (tag 997).</summary>
		FIX_TimeUnit = 997,

		/// <summary>FIX UnderlyingUnitOfMeasure tag (tag 998).</summary>
		FIX_UnderlyingUnitOfMeasure = 998,

		/// <summary>FIX LegUnitOfMeasure tag (tag 999).</summary>
		FIX_LegUnitOfMeasure = 999,

		/// <summary>FIX UnderlyingTimeUnit tag (tag 1000).</summary>
		FIX_UnderlyingTimeUnit = 1000,

		/// <summary>FIX LegTimeUnit tag (tag 1001).</summary>
		FIX_LegTimeUnit = 1001,

		/// <summary>FIX AllocMethod tag (tag 1002).</summary>
		FIX_AllocMethod = 1002,

		/// <summary>FIX TradeID tag (tag 1003).</summary>
		FIX_TradeID = 1003,

		/// <summary>FIX SideTradeReportID tag (tag 1005).</summary>
		FIX_SideTradeReportID = 1005,

		/// <summary>FIX SideFillStationCd tag (tag 1006).</summary>
		FIX_SideFillStationCd = 1006,

		/// <summary>FIX SideReasonCd tag (tag 1007).</summary>
		FIX_SideReasonCd = 1007,

		/// <summary>FIX SideTrdSubTyp tag (tag 1008).</summary>
		FIX_SideTrdSubTyp = 1008,

		/// <summary>FIX SideLastQty tag (tag 1009).</summary>
		FIX_SideLastQty = 1009,

		/// <summary>FIX MessageEventSource tag (tag 1011).</summary>
		FIX_MessageEventSource = 1011,

		/// <summary>FIX SideTrdRegTimestamp tag (tag 1012).</summary>
		FIX_SideTrdRegTimestamp = 1012,

		/// <summary>FIX SideTrdRegTimestampType tag (tag 1013).</summary>
		FIX_SideTrdRegTimestampType = 1013,

		/// <summary>FIX SideTrdRegTimestampSrc tag (tag 1014).</summary>
		FIX_SideTrdRegTimestampSrc = 1014,

		/// <summary>FIX AsOfIndicator tag (tag 1015).</summary>
		FIX_AsOfIndicator = 1015,

		/// <summary>FIX NoSideTrdRegTS tag (tag 1016).</summary>
		FIX_NoSideTrdRegTS = 1016,

		/// <summary>FIX LegOptionRatio tag (tag 1017).</summary>
		FIX_LegOptionRatio = 1017,

		/// <summary>FIX NoInstrumentParties tag (tag 1018).</summary>
		FIX_NoInstrumentParties = 1018,

		/// <summary>FIX InstrumentPartyID tag (tag 1019).</summary>
		FIX_InstrumentPartyID = 1019,

		/// <summary>FIX TradeVolume tag (tag 1020).</summary>
		FIX_TradeVolume = 1020,

		/// <summary>FIX MDBookType tag (tag 1021).</summary>
		FIX_MDBookType = 1021,

		/// <summary>FIX MDFeedType tag (tag 1022).</summary>
		FIX_MDFeedType = 1022,

		/// <summary>FIX MDPriceLevel tag (tag 1023).</summary>
		FIX_MDPriceLevel = 1023,

		/// <summary>FIX MDOriginType tag (tag 1024).</summary>
		FIX_MDOriginType = 1024,

		/// <summary>FIX FirstPx tag (tag 1025).</summary>
		FIX_FirstPx = 1025,

		/// <summary>FIX MDEntrySpotRate tag (tag 1026).</summary>
		FIX_MDEntrySpotRate = 1026,

		/// <summary>FIX MDEntryForwardPoints tag (tag 1027).</summary>
		FIX_MDEntryForwardPoints = 1027,

		/// <summary>FIX ManualOrderIndicator tag (tag 1028).</summary>
		FIX_ManualOrderIndicator = 1028,

		/// <summary>FIX CustDirectedOrder tag (tag 1029).</summary>
		FIX_CustDirectedOrder = 1029,

		/// <summary>FIX ReceivedDeptID tag (tag 1030).</summary>
		FIX_ReceivedDeptID = 1030,

		/// <summary>FIX CustOrderHandlingInst tag (tag 1031).</summary>
		FIX_CustOrderHandlingInst = 1031,

		/// <summary>FIX OrderHandlingInstSource tag (tag 1032).</summary>
		FIX_OrderHandlingInstSource = 1032,

		/// <summary>FIX DeskType tag (tag 1033).</summary>
		FIX_DeskType = 1033,

		/// <summary>FIX DeskTypeSource tag (tag 1034).</summary>
		FIX_DeskTypeSource = 1034,

		/// <summary>FIX DeskOrderHandlingInst tag (tag 1035).</summary>
		FIX_DeskOrderHandlingInst = 1035,

		/// <summary>FIX ExecAckStatus tag (tag 1036).</summary>
		FIX_ExecAckStatus = 1036,

		/// <summary>FIX UnderlyingDeliveryAmount tag (tag 1037).</summary>
		FIX_UnderlyingDeliveryAmount = 1037,

		/// <summary>FIX UnderlyingCapValue tag (tag 1038).</summary>
		FIX_UnderlyingCapValue = 1038,

		/// <summary>FIX UnderlyingSettlMethod tag (tag 1039).</summary>
		FIX_UnderlyingSettlMethod = 1039,

		/// <summary>FIX SecondaryTradeID tag (tag 1040).</summary>
		FIX_SecondaryTradeID = 1040,

		/// <summary>FIX FirmTradeID tag (tag 1041).</summary>
		FIX_FirmTradeID = 1041,

		/// <summary>FIX SecondaryFirmTradeID tag (tag 1042).</summary>
		FIX_SecondaryFirmTradeID = 1042,

		/// <summary>FIX CollApplType tag (tag 1043).</summary>
		FIX_CollApplType = 1043,

		/// <summary>FIX UnderlyingAdjustedQuantity tag (tag 1044).</summary>
		FIX_UnderlyingAdjustedQuantity = 1044,

		/// <summary>FIX UnderlyingFXRate tag (tag 1045).</summary>
		FIX_UnderlyingFXRate = 1045,

		/// <summary>FIX UnderlyingFXRateCalc tag (tag 1046).</summary>
		FIX_UnderlyingFXRateCalc = 1046,

		/// <summary>FIX AllocPositionEffect tag (tag 1047).</summary>
		FIX_AllocPositionEffect = 1047,

		/// <summary>FIX DealingCapacity tag (tag 1048).</summary>
		FIX_DealingCapacity = 1048,

		/// <summary>FIX InstrmtAssignmentMethod tag (tag 1049).</summary>
		FIX_InstrmtAssignmentMethod = 1049,

		/// <summary>FIX InstrumentPartyIDSource tag (tag 1050).</summary>
		FIX_InstrumentPartyIDSource = 1050,

		/// <summary>FIX InstrumentPartyRole tag (tag 1051).</summary>
		FIX_InstrumentPartyRole = 1051,

		/// <summary>FIX NoInstrumentPartySubIDs tag (tag 1052).</summary>
		FIX_NoInstrumentPartySubIDs = 1052,

		/// <summary>FIX InstrumentPartySubID tag (tag 1053).</summary>
		FIX_InstrumentPartySubID = 1053,

		/// <summary>FIX InstrumentPartySubIDType tag (tag 1054).</summary>
		FIX_InstrumentPartySubIDType = 1054,

		/// <summary>FIX PositionCurrency tag (tag 1055).</summary>
		FIX_PositionCurrency = 1055,

		/// <summary>FIX CalculatedCcyLastQty tag (tag 1056).</summary>
		FIX_CalculatedCcyLastQty = 1056,

		/// <summary>FIX AggressorIndicator tag (tag 1057).</summary>
		FIX_AggressorIndicator = 1057,

		/// <summary>FIX NoUndlyInstrumentParties tag (tag 1058).</summary>
		FIX_NoUndlyInstrumentParties = 1058,

		/// <summary>FIX UnderlyingInstrumentPartyID tag (tag 1059).</summary>
		FIX_UnderlyingInstrumentPartyID = 1059,

		/// <summary>FIX UnderlyingInstrumentPartyIDSource tag (tag 1060).</summary>
		FIX_UnderlyingInstrumentPartyIDSource = 1060,

		/// <summary>FIX UnderlyingInstrumentPartyRole tag (tag 1061).</summary>
		FIX_UnderlyingInstrumentPartyRole = 1061,

		/// <summary>FIX NoUndlyInstrumentPartySubIDs tag (tag 1062).</summary>
		FIX_NoUndlyInstrumentPartySubIDs = 1062,

		/// <summary>FIX UnderlyingInstrumentPartySubID tag (tag 1063).</summary>
		FIX_UnderlyingInstrumentPartySubID = 1063,

		/// <summary>FIX UnderlyingInstrumentPartySubIDType tag (tag 1064).</summary>
		FIX_UnderlyingInstrumentPartySubIDType = 1064,

		/// <summary>FIX BidSwapPoints tag (tag 1065).</summary>
		FIX_BidSwapPoints = 1065,

		/// <summary>FIX OfferSwapPoints tag (tag 1066).</summary>
		FIX_OfferSwapPoints = 1066,

		/// <summary>FIX LegBidForwardPoints tag (tag 1067).</summary>
		FIX_LegBidForwardPoints = 1067,

		/// <summary>FIX LegOfferForwardPoints tag (tag 1068).</summary>
		FIX_LegOfferForwardPoints = 1068,

		/// <summary>FIX SwapPoints tag (tag 1069).</summary>
		FIX_SwapPoints = 1069,

		/// <summary>FIX MDQuoteType tag (tag 1070).</summary>
		FIX_MDQuoteType = 1070,

		/// <summary>FIX LastSwapPoints tag (tag 1071).</summary>
		FIX_LastSwapPoints = 1071,

		/// <summary>FIX SideGrossTradeAmt tag (tag 1072).</summary>
		FIX_SideGrossTradeAmt = 1072,

		/// <summary>FIX LegLastForwardPoints tag (tag 1073).</summary>
		FIX_LegLastForwardPoints = 1073,

		/// <summary>FIX LegCalculatedCcyLastQty tag (tag 1074).</summary>
		FIX_LegCalculatedCcyLastQty = 1074,

		/// <summary>FIX LegGrossTradeAmt tag (tag 1075).</summary>
		FIX_LegGrossTradeAmt = 1075,

		/// <summary>FIX MaturityTime tag (tag 1079).</summary>
		FIX_MaturityTime = 1079,

		/// <summary>FIX RefOrderID tag (tag 1080).</summary>
		FIX_RefOrderID = 1080,

		/// <summary>FIX RefOrderIDSource tag (tag 1081).</summary>
		FIX_RefOrderIDSource = 1081,

		/// <summary>FIX SecondaryDisplayQty tag (tag 1082).</summary>
		FIX_SecondaryDisplayQty = 1082,

		/// <summary>FIX DisplayWhen tag (tag 1083).</summary>
		FIX_DisplayWhen = 1083,

		/// <summary>FIX DisplayMethod tag (tag 1084).</summary>
		FIX_DisplayMethod = 1084,

		/// <summary>FIX DisplayLowQty tag (tag 1085).</summary>
		FIX_DisplayLowQty = 1085,

		/// <summary>FIX DisplayHighQty tag (tag 1086).</summary>
		FIX_DisplayHighQty = 1086,

		/// <summary>FIX DisplayMinIncr tag (tag 1087).</summary>
		FIX_DisplayMinIncr = 1087,

		/// <summary>FIX RefreshQty tag (tag 1088).</summary>
		FIX_RefreshQty = 1088,

		/// <summary>FIX MatchIncrement tag (tag 1089).</summary>
		FIX_MatchIncrement = 1089,

		/// <summary>FIX MaxPriceLevels tag (tag 1090).</summary>
		FIX_MaxPriceLevels = 1090,

		/// <summary>FIX PreTradeAnonymity tag (tag 1091).</summary>
		FIX_PreTradeAnonymity = 1091,

		/// <summary>FIX PriceProtectionScope tag (tag 1092).</summary>
		FIX_PriceProtectionScope = 1092,

		/// <summary>FIX LotType tag (tag 1093).</summary>
		FIX_LotType = 1093,

		/// <summary>FIX PegPriceType tag (tag 1094).</summary>
		FIX_PegPriceType = 1094,

		/// <summary>FIX PeggedRefPrice tag (tag 1095).</summary>
		FIX_PeggedRefPrice = 1095,

		/// <summary>FIX PegSecurityIDSource tag (tag 1096).</summary>
		FIX_PegSecurityIDSource = 1096,

		/// <summary>FIX PegSecurityID tag (tag 1097).</summary>
		FIX_PegSecurityID = 1097,

		/// <summary>FIX PegSymbol tag (tag 1098).</summary>
		FIX_PegSymbol = 1098,

		/// <summary>FIX PegSecurityDesc tag (tag 1099).</summary>
		FIX_PegSecurityDesc = 1099,

		/// <summary>FIX TriggerType tag (tag 1100).</summary>
		FIX_TriggerType = 1100,

		/// <summary>FIX TriggerAction tag (tag 1101).</summary>
		FIX_TriggerAction = 1101,

		/// <summary>FIX TriggerPrice tag (tag 1102).</summary>
		FIX_TriggerPrice = 1102,

		/// <summary>FIX TriggerSymbol tag (tag 1103).</summary>
		FIX_TriggerSymbol = 1103,

		/// <summary>FIX TriggerSecurityID tag (tag 1104).</summary>
		FIX_TriggerSecurityID = 1104,

		/// <summary>FIX TriggerSecurityIDSource tag (tag 1105).</summary>
		FIX_TriggerSecurityIDSource = 1105,

		/// <summary>FIX TriggerSecurityDesc tag (tag 1106).</summary>
		FIX_TriggerSecurityDesc = 1106,

		/// <summary>FIX TriggerPriceType tag (tag 1107).</summary>
		FIX_TriggerPriceType = 1107,

		/// <summary>FIX TriggerPriceTypeScope tag (tag 1108).</summary>
		FIX_TriggerPriceTypeScope = 1108,

		/// <summary>FIX TriggerPriceDirection tag (tag 1109).</summary>
		FIX_TriggerPriceDirection = 1109,

		/// <summary>FIX TriggerNewPrice tag (tag 1110).</summary>
		FIX_TriggerNewPrice = 1110,

		/// <summary>FIX TriggerOrderType tag (tag 1111).</summary>
		FIX_TriggerOrderType = 1111,

		/// <summary>FIX TriggerNewQty tag (tag 1112).</summary>
		FIX_TriggerNewQty = 1112,

		/// <summary>FIX TriggerTradingSessionID tag (tag 1113).</summary>
		FIX_TriggerTradingSessionID = 1113,

		/// <summary>FIX TriggerTradingSessionSubID tag (tag 1114).</summary>
		FIX_TriggerTradingSessionSubID = 1114,

		/// <summary>FIX OrderCategory tag (tag 1115).</summary>
		FIX_OrderCategory = 1115,

		/// <summary>FIX NoRootPartyIDs tag (tag 1116).</summary>
		FIX_NoRootPartyIDs = 1116,

		/// <summary>FIX RootPartyID tag (tag 1117).</summary>
		FIX_RootPartyID = 1117,

		/// <summary>FIX RootPartyIDSource tag (tag 1118).</summary>
		FIX_RootPartyIDSource = 1118,

		/// <summary>FIX RootPartyRole tag (tag 1119).</summary>
		FIX_RootPartyRole = 1119,

		/// <summary>FIX NoRootPartySubIDs tag (tag 1120).</summary>
		FIX_NoRootPartySubIDs = 1120,

		/// <summary>FIX RootPartySubID tag (tag 1121).</summary>
		FIX_RootPartySubID = 1121,

		/// <summary>FIX RootPartySubIDType tag (tag 1122).</summary>
		FIX_RootPartySubIDType = 1122,

		/// <summary>FIX TradeHandlingInstr tag (tag 1123).</summary>
		FIX_TradeHandlingInstr = 1123,

		/// <summary>FIX OrigTradeHandlingInstr tag (tag 1124).</summary>
		FIX_OrigTradeHandlingInstr = 1124,

		/// <summary>FIX OrigTradeDate tag (tag 1125).</summary>
		FIX_OrigTradeDate = 1125,

		/// <summary>FIX OrigTradeID tag (tag 1126).</summary>
		FIX_OrigTradeID = 1126,

		/// <summary>FIX OrigSecondaryTradeID tag (tag 1127).</summary>
		FIX_OrigSecondaryTradeID = 1127,

		/// <summary>FIX ApplVerID tag (tag 1128).</summary>
		FIX_ApplVerID = 1128,

		/// <summary>FIX CstmApplVerID tag (tag 1129).</summary>
		FIX_CstmApplVerID = 1129,

		/// <summary>FIX RefApplVerID tag (tag 1130).</summary>
		FIX_RefApplVerID = 1130,

		/// <summary>FIX RefCstmApplVerID tag (tag 1131).</summary>
		FIX_RefCstmApplVerID = 1131,

		/// <summary>FIX TZTransactTime tag (tag 1132).</summary>
		FIX_TZTransactTime = 1132,

		/// <summary>FIX ExDestinationIDSource tag (tag 1133).</summary>
		FIX_ExDestinationIDSource = 1133,

		/// <summary>FIX ReportedPxDiff tag (tag 1134).</summary>
		FIX_ReportedPxDiff = 1134,

		/// <summary>FIX RptSys tag (tag 1135).</summary>
		FIX_RptSys = 1135,

		/// <summary>FIX AllocClearingFeeIndicator tag (tag 1136).</summary>
		FIX_AllocClearingFeeIndicator = 1136,

		/// <summary>FIX DefaultApplVerID tag (tag 1137).</summary>
		FIX_DefaultApplVerID = 1137,

		/// <summary>FIX DisplayQty tag (tag 1138).</summary>
		FIX_DisplayQty = 1138,

		/// <summary>FIX ExchangeSpecialInstructions tag (tag 1139).</summary>
		FIX_ExchangeSpecialInstructions = 1139,

		/// <summary>FIX UnderlyingMaturityTime tag (tag 1213).</summary>
		FIX_UnderlyingMaturityTime = 1213,

		/// <summary>FIX LegMaturityTime tag (tag 1212).</summary>
		FIX_LegMaturityTime = 1212,

		/// <summary>FIX MaxTradeVol tag (tag 1140).</summary>
		FIX_MaxTradeVol = 1140,

		/// <summary>FIX NoMDFeedTypes tag (tag 1141).</summary>
		FIX_NoMDFeedTypes = 1141,

		/// <summary>FIX MatchAlgorithm tag (tag 1142).</summary>
		FIX_MatchAlgorithm = 1142,

		/// <summary>FIX MaxPriceVariation tag (tag 1143).</summary>
		FIX_MaxPriceVariation = 1143,

		/// <summary>FIX ImpliedMarketIndicator tag (tag 1144).</summary>
		FIX_ImpliedMarketIndicator = 1144,

		/// <summary>FIX EventTime tag (tag 1145).</summary>
		FIX_EventTime = 1145,

		/// <summary>FIX MinPriceIncrementAmount tag (tag 1146).</summary>
		FIX_MinPriceIncrementAmount = 1146,

		/// <summary>FIX UnitOfMeasureQty tag (tag 1147).</summary>
		FIX_UnitOfMeasureQty = 1147,

		/// <summary>FIX LowLimitPrice tag (tag 1148).</summary>
		FIX_LowLimitPrice = 1148,

		/// <summary>FIX HighLimitPrice tag (tag 1149).</summary>
		FIX_HighLimitPrice = 1149,

		/// <summary>FIX TradingReferencePrice tag (tag 1150).</summary>
		FIX_TradingReferencePrice = 1150,

		/// <summary>FIX SecurityGroup tag (tag 1151).</summary>
		FIX_SecurityGroup = 1151,

		/// <summary>FIX LegNumber tag (tag 1152).</summary>
		FIX_LegNumber = 1152,

		/// <summary>FIX SettlementCycleNo tag (tag 1153).</summary>
		FIX_SettlementCycleNo = 1153,

		/// <summary>FIX SideCurrency tag (tag 1154).</summary>
		FIX_SideCurrency = 1154,

		/// <summary>FIX SideSettlCurrency tag (tag 1155).</summary>
		FIX_SideSettlCurrency = 1155,

		/// <summary>FIX CcyAmt tag (tag 1157).</summary>
		FIX_CcyAmt = 1157,

		/// <summary>FIX NoSettlDetails tag (tag 1158).</summary>
		FIX_NoSettlDetails = 1158,

		/// <summary>FIX SettlObligMode tag (tag 1159).</summary>
		FIX_SettlObligMode = 1159,

		/// <summary>FIX SettlObligMsgID tag (tag 1160).</summary>
		FIX_SettlObligMsgID = 1160,

		/// <summary>FIX SettlObligID tag (tag 1161).</summary>
		FIX_SettlObligID = 1161,

		/// <summary>FIX SettlObligTransType tag (tag 1162).</summary>
		FIX_SettlObligTransType = 1162,

		/// <summary>FIX SettlObligRefID tag (tag 1163).</summary>
		FIX_SettlObligRefID = 1163,

		/// <summary>FIX SettlObligSource tag (tag 1164).</summary>
		FIX_SettlObligSource = 1164,

		/// <summary>FIX NoSettlOblig tag (tag 1165).</summary>
		FIX_NoSettlOblig = 1165,

		/// <summary>FIX QuoteMsgID tag (tag 1166).</summary>
		FIX_QuoteMsgID = 1166,

		/// <summary>FIX QuoteEntryStatus tag (tag 1167).</summary>
		FIX_QuoteEntryStatus = 1167,

		/// <summary>FIX TotNoCxldQuotes tag (tag 1168).</summary>
		FIX_TotNoCxldQuotes = 1168,

		/// <summary>FIX TotNoAccQuotes tag (tag 1169).</summary>
		FIX_TotNoAccQuotes = 1169,

		/// <summary>FIX TotNoRejQuotes tag (tag 1170).</summary>
		FIX_TotNoRejQuotes = 1170,

		/// <summary>FIX PrivateQuote tag (tag 1171).</summary>
		FIX_PrivateQuote = 1171,

		/// <summary>FIX RespondentType tag (tag 1172).</summary>
		FIX_RespondentType = 1172,

		/// <summary>FIX MDSubBookType tag (tag 1173).</summary>
		FIX_MDSubBookType = 1173,

		/// <summary>FIX SecurityTradingEvent tag (tag 1174).</summary>
		FIX_SecurityTradingEvent = 1174,

		/// <summary>FIX NoStatsIndicators tag (tag 1175).</summary>
		FIX_NoStatsIndicators = 1175,

		/// <summary>FIX StatsType tag (tag 1176).</summary>
		FIX_StatsType = 1176,

		/// <summary>FIX NoOfSecSizes tag (tag 1177).</summary>
		FIX_NoOfSecSizes = 1177,

		/// <summary>FIX MDSecSizeType tag (tag 1178).</summary>
		FIX_MDSecSizeType = 1178,

		/// <summary>FIX MDSecSize tag (tag 1179).</summary>
		FIX_MDSecSize = 1179,

		/// <summary>FIX ApplID tag (tag 1180).</summary>
		FIX_ApplID = 1180,

		/// <summary>FIX ApplSeqNum tag (tag 1181).</summary>
		FIX_ApplSeqNum = 1181,

		/// <summary>FIX ApplBegSeqNum tag (tag 1182).</summary>
		FIX_ApplBegSeqNum = 1182,

		/// <summary>FIX ApplEndSeqNum tag (tag 1183).</summary>
		FIX_ApplEndSeqNum = 1183,

		/// <summary>FIX SecurityXMLLen tag (tag 1184).</summary>
		FIX_SecurityXMLLen = 1184,

		/// <summary>FIX SecurityXML tag (tag 1185).</summary>
		FIX_SecurityXML = 1185,

		/// <summary>FIX SecurityXMLSchema tag (tag 1186).</summary>
		FIX_SecurityXMLSchema = 1186,

		/// <summary>FIX RefreshIndicator tag (tag 1187).</summary>
		FIX_RefreshIndicator = 1187,

		/// <summary>FIX Volatility tag (tag 1188).</summary>
		FIX_Volatility = 1188,

		/// <summary>FIX TimeToExpiration tag (tag 1189).</summary>
		FIX_TimeToExpiration = 1189,

		/// <summary>FIX RiskFreeRate tag (tag 1190).</summary>
		FIX_RiskFreeRate = 1190,

		/// <summary>FIX PriceUnitOfMeasure tag (tag 1191).</summary>
		FIX_PriceUnitOfMeasure = 1191,

		/// <summary>FIX PriceUnitOfMeasureQty tag (tag 1192).</summary>
		FIX_PriceUnitOfMeasureQty = 1192,

		/// <summary>FIX SettlMethod tag (tag 1193).</summary>
		FIX_SettlMethod = 1193,

		/// <summary>FIX ExerciseStyle tag (tag 1194).</summary>
		FIX_ExerciseStyle = 1194,

		/// <summary>FIX UnderlyingExerciseStyle tag (tag 1419).</summary>
		FIX_UnderlyingExerciseStyle = 1419,

		/// <summary>FIX LegExerciseStyle tag (tag 1420).</summary>
		FIX_LegExerciseStyle = 1420,

		/// <summary>FIX OptPayoutAmount tag (tag 1195).</summary>
		FIX_OptPayoutAmount = 1195,

		/// <summary>FIX PriceQuoteMethod tag (tag 1196).</summary>
		FIX_PriceQuoteMethod = 1196,

		/// <summary>FIX ValuationMethod tag (tag 1197).</summary>
		FIX_ValuationMethod = 1197,

		/// <summary>FIX ListMethod tag (tag 1198).</summary>
		FIX_ListMethod = 1198,

		/// <summary>FIX CapPrice tag (tag 1199).</summary>
		FIX_CapPrice = 1199,

		/// <summary>FIX FloorPrice tag (tag 1200).</summary>
		FIX_FloorPrice = 1200,

		/// <summary>FIX NoStrikeRules tag (tag 1201).</summary>
		FIX_NoStrikeRules = 1201,

		/// <summary>FIX StartStrikePxRange tag (tag 1202).</summary>
		FIX_StartStrikePxRange = 1202,

		/// <summary>FIX EndStrikePxRange tag (tag 1203).</summary>
		FIX_EndStrikePxRange = 1203,

		/// <summary>FIX StrikeIncrement tag (tag 1204).</summary>
		FIX_StrikeIncrement = 1204,

		/// <summary>FIX NoTickRules tag (tag 1205).</summary>
		FIX_NoTickRules = 1205,

		/// <summary>FIX StartTickPriceRange tag (tag 1206).</summary>
		FIX_StartTickPriceRange = 1206,

		/// <summary>FIX EndTickPriceRange tag (tag 1207).</summary>
		FIX_EndTickPriceRange = 1207,

		/// <summary>FIX TickIncrement tag (tag 1208).</summary>
		FIX_TickIncrement = 1208,

		/// <summary>FIX TickRuleType tag (tag 1209).</summary>
		FIX_TickRuleType = 1209,

		/// <summary>FIX NestedInstrAttribType tag (tag 1210).</summary>
		FIX_NestedInstrAttribType = 1210,

		/// <summary>FIX NestedInstrAttribValue tag (tag 1211).</summary>
		FIX_NestedInstrAttribValue = 1211,

		/// <summary>FIX DerivativeSymbol tag (tag 1214).</summary>
		FIX_DerivativeSymbol = 1214,

		/// <summary>FIX DerivativeSymbolSfx tag (tag 1215).</summary>
		FIX_DerivativeSymbolSfx = 1215,

		/// <summary>FIX DerivativeSecurityID tag (tag 1216).</summary>
		FIX_DerivativeSecurityID = 1216,

		/// <summary>FIX DerivativeSecurityIDSource tag (tag 1217).</summary>
		FIX_DerivativeSecurityIDSource = 1217,

		/// <summary>FIX NoDerivativeSecurityAltID tag (tag 1218).</summary>
		FIX_NoDerivativeSecurityAltID = 1218,

		/// <summary>FIX DerivativeSecurityAltID tag (tag 1219).</summary>
		FIX_DerivativeSecurityAltID = 1219,

		/// <summary>FIX DerivativeSecurityAltIDSource tag (tag 1220).</summary>
		FIX_DerivativeSecurityAltIDSource = 1220,

		/// <summary>FIX SecondaryLowLimitPrice tag (tag 1221).</summary>
		FIX_SecondaryLowLimitPrice = 1221,

		/// <summary>FIX SecondaryHighLimitPrice tag (tag 1230).</summary>
		FIX_SecondaryHighLimitPrice = 1230,

		/// <summary>FIX MaturityRuleID tag (tag 1222).</summary>
		FIX_MaturityRuleID = 1222,

		/// <summary>FIX StrikeRuleID tag (tag 1223).</summary>
		FIX_StrikeRuleID = 1223,

		/// <summary>FIX DerivativeOptPayAmount tag (tag 1225).</summary>
		FIX_DerivativeOptPayAmount = 1225,

		/// <summary>FIX EndMaturityMonthYear tag (tag 1226).</summary>
		FIX_EndMaturityMonthYear = 1226,

		/// <summary>FIX ProductComplex tag (tag 1227).</summary>
		FIX_ProductComplex = 1227,

		/// <summary>FIX DerivativeProductComplex tag (tag 1228).</summary>
		FIX_DerivativeProductComplex = 1228,

		/// <summary>FIX MaturityMonthYearIncrement tag (tag 1229).</summary>
		FIX_MaturityMonthYearIncrement = 1229,

		/// <summary>FIX MinLotSize tag (tag 1231).</summary>
		FIX_MinLotSize = 1231,

		/// <summary>FIX NoExecInstRules tag (tag 1232).</summary>
		FIX_NoExecInstRules = 1232,

		/// <summary>FIX NoLotTypeRules tag (tag 1234).</summary>
		FIX_NoLotTypeRules = 1234,

		/// <summary>FIX NoMatchRules tag (tag 1235).</summary>
		FIX_NoMatchRules = 1235,

		/// <summary>FIX NoMaturityRules tag (tag 1236).</summary>
		FIX_NoMaturityRules = 1236,

		/// <summary>FIX NoOrdTypeRules tag (tag 1237).</summary>
		FIX_NoOrdTypeRules = 1237,

		/// <summary>FIX NoTimeInForceRules tag (tag 1239).</summary>
		FIX_NoTimeInForceRules = 1239,

		/// <summary>FIX SecondaryTradingReferencePrice tag (tag 1240).</summary>
		FIX_SecondaryTradingReferencePrice = 1240,

		/// <summary>FIX StartMaturityMonthYear tag (tag 1241).</summary>
		FIX_StartMaturityMonthYear = 1241,

		/// <summary>FIX FlexProductEligibilityIndicator tag (tag 1242).</summary>
		FIX_FlexProductEligibilityIndicator = 1242,

		/// <summary>FIX DerivFlexProductEligibilityIndicator tag (tag 1243).</summary>
		FIX_DerivFlexProductEligibilityIndicator = 1243,

		/// <summary>FIX FlexibleIndicator tag (tag 1244).</summary>
		FIX_FlexibleIndicator = 1244,

		/// <summary>FIX TradingCurrency tag (tag 1245).</summary>
		FIX_TradingCurrency = 1245,

		/// <summary>FIX DerivativeProduct tag (tag 1246).</summary>
		FIX_DerivativeProduct = 1246,

		/// <summary>FIX DerivativeSecurityGroup tag (tag 1247).</summary>
		FIX_DerivativeSecurityGroup = 1247,

		/// <summary>FIX DerivativeCFICode tag (tag 1248).</summary>
		FIX_DerivativeCFICode = 1248,

		/// <summary>FIX DerivativeSecurityType tag (tag 1249).</summary>
		FIX_DerivativeSecurityType = 1249,

		/// <summary>FIX DerivativeSecuritySubType tag (tag 1250).</summary>
		FIX_DerivativeSecuritySubType = 1250,

		/// <summary>FIX DerivativeMaturityMonthYear tag (tag 1251).</summary>
		FIX_DerivativeMaturityMonthYear = 1251,

		/// <summary>FIX DerivativeMaturityDate tag (tag 1252).</summary>
		FIX_DerivativeMaturityDate = 1252,

		/// <summary>FIX DerivativeMaturityTime tag (tag 1253).</summary>
		FIX_DerivativeMaturityTime = 1253,

		/// <summary>FIX DerivativeSettleOnOpenFlag tag (tag 1254).</summary>
		FIX_DerivativeSettleOnOpenFlag = 1254,

		/// <summary>FIX DerivativeInstrmtAssignmentMethod tag (tag 1255).</summary>
		FIX_DerivativeInstrmtAssignmentMethod = 1255,

		/// <summary>FIX DerivativeSecurityStatus tag (tag 1256).</summary>
		FIX_DerivativeSecurityStatus = 1256,

		/// <summary>FIX DerivativeInstrRegistry tag (tag 1257).</summary>
		FIX_DerivativeInstrRegistry = 1257,

		/// <summary>FIX DerivativeCountryOfIssue tag (tag 1258).</summary>
		FIX_DerivativeCountryOfIssue = 1258,

		/// <summary>FIX DerivativeStateOrProvinceOfIssue tag (tag 1259).</summary>
		FIX_DerivativeStateOrProvinceOfIssue = 1259,

		/// <summary>FIX DerivativeLocaleOfIssue tag (tag 1260).</summary>
		FIX_DerivativeLocaleOfIssue = 1260,

		/// <summary>FIX DerivativeStrikePrice tag (tag 1261).</summary>
		FIX_DerivativeStrikePrice = 1261,

		/// <summary>FIX DerivativeStrikeCurrency tag (tag 1262).</summary>
		FIX_DerivativeStrikeCurrency = 1262,

		/// <summary>FIX DerivativeStrikeMultiplier tag (tag 1263).</summary>
		FIX_DerivativeStrikeMultiplier = 1263,

		/// <summary>FIX DerivativeStrikeValue tag (tag 1264).</summary>
		FIX_DerivativeStrikeValue = 1264,

		/// <summary>FIX DerivativeOptAttribute tag (tag 1265).</summary>
		FIX_DerivativeOptAttribute = 1265,

		/// <summary>FIX DerivativeContractMultiplier tag (tag 1266).</summary>
		FIX_DerivativeContractMultiplier = 1266,

		/// <summary>FIX DerivativeMinPriceIncrement tag (tag 1267).</summary>
		FIX_DerivativeMinPriceIncrement = 1267,

		/// <summary>FIX DerivativeMinPriceIncrementAmount tag (tag 1268).</summary>
		FIX_DerivativeMinPriceIncrementAmount = 1268,

		/// <summary>FIX DerivativeUnitOfMeasure tag (tag 1269).</summary>
		FIX_DerivativeUnitOfMeasure = 1269,

		/// <summary>FIX DerivativeUnitOfMeasureQty tag (tag 1270).</summary>
		FIX_DerivativeUnitOfMeasureQty = 1270,

		/// <summary>FIX DerivativeTimeUnit tag (tag 1271).</summary>
		FIX_DerivativeTimeUnit = 1271,

		/// <summary>FIX DerivativeSecurityExchange tag (tag 1272).</summary>
		FIX_DerivativeSecurityExchange = 1272,

		/// <summary>FIX DerivativePositionLimit tag (tag 1273).</summary>
		FIX_DerivativePositionLimit = 1273,

		/// <summary>FIX DerivativeNTPositionLimit tag (tag 1274).</summary>
		FIX_DerivativeNTPositionLimit = 1274,

		/// <summary>FIX DerivativeIssuer tag (tag 1275).</summary>
		FIX_DerivativeIssuer = 1275,

		/// <summary>FIX DerivativeIssueDate tag (tag 1276).</summary>
		FIX_DerivativeIssueDate = 1276,

		/// <summary>FIX DerivativeEncodedIssuerLen tag (tag 1277).</summary>
		FIX_DerivativeEncodedIssuerLen = 1277,

		/// <summary>FIX DerivativeEncodedIssuer tag (tag 1278).</summary>
		FIX_DerivativeEncodedIssuer = 1278,

		/// <summary>FIX DerivativeSecurityDesc tag (tag 1279).</summary>
		FIX_DerivativeSecurityDesc = 1279,

		/// <summary>FIX DerivativeEncodedSecurityDescLen tag (tag 1280).</summary>
		FIX_DerivativeEncodedSecurityDescLen = 1280,

		/// <summary>FIX DerivativeEncodedSecurityDesc tag (tag 1281).</summary>
		FIX_DerivativeEncodedSecurityDesc = 1281,

		/// <summary>FIX DerivativeSecurityXMLLen tag (tag 1282).</summary>
		FIX_DerivativeSecurityXMLLen = 1282,

		/// <summary>FIX DerivativeSecurityXML tag (tag 1283).</summary>
		FIX_DerivativeSecurityXML = 1283,

		/// <summary>FIX DerivativeSecurityXMLSchema tag (tag 1284).</summary>
		FIX_DerivativeSecurityXMLSchema = 1284,

		/// <summary>FIX DerivativeContractSettlMonth tag (tag 1285).</summary>
		FIX_DerivativeContractSettlMonth = 1285,

		/// <summary>FIX NoDerivativeEvents tag (tag 1286).</summary>
		FIX_NoDerivativeEvents = 1286,

		/// <summary>FIX DerivativeEventType tag (tag 1287).</summary>
		FIX_DerivativeEventType = 1287,

		/// <summary>FIX DerivativeEventDate tag (tag 1288).</summary>
		FIX_DerivativeEventDate = 1288,

		/// <summary>FIX DerivativeEventTime tag (tag 1289).</summary>
		FIX_DerivativeEventTime = 1289,

		/// <summary>FIX DerivativeEventPx tag (tag 1290).</summary>
		FIX_DerivativeEventPx = 1290,

		/// <summary>FIX DerivativeEventText tag (tag 1291).</summary>
		FIX_DerivativeEventText = 1291,

		/// <summary>FIX NoDerivativeInstrumentParties tag (tag 1292).</summary>
		FIX_NoDerivativeInstrumentParties = 1292,

		/// <summary>FIX DerivativeInstrumentPartyID tag (tag 1293).</summary>
		FIX_DerivativeInstrumentPartyID = 1293,

		/// <summary>FIX DerivativeInstrumentPartyIDSource tag (tag 1294).</summary>
		FIX_DerivativeInstrumentPartyIDSource = 1294,

		/// <summary>FIX DerivativeInstrumentPartyRole tag (tag 1295).</summary>
		FIX_DerivativeInstrumentPartyRole = 1295,

		/// <summary>FIX NoDerivativeInstrumentPartySubIDs tag (tag 1296).</summary>
		FIX_NoDerivativeInstrumentPartySubIDs = 1296,

		/// <summary>FIX DerivativeInstrumentPartySubID tag (tag 1297).</summary>
		FIX_DerivativeInstrumentPartySubID = 1297,

		/// <summary>FIX DerivativeInstrumentPartySubIDType tag (tag 1298).</summary>
		FIX_DerivativeInstrumentPartySubIDType = 1298,

		/// <summary>FIX DerivativeExerciseStyle tag (tag 1299).</summary>
		FIX_DerivativeExerciseStyle = 1299,

		/// <summary>FIX MarketSegmentID tag (tag 1300).</summary>
		FIX_MarketSegmentID = 1300,

		/// <summary>FIX MarketID tag (tag 1301).</summary>
		FIX_MarketID = 1301,

		/// <summary>FIX MaturityMonthYearIncrementUnits tag (tag 1302).</summary>
		FIX_MaturityMonthYearIncrementUnits = 1302,

		/// <summary>FIX MaturityMonthYearFormat tag (tag 1303).</summary>
		FIX_MaturityMonthYearFormat = 1303,

		/// <summary>FIX StrikeExerciseStyle tag (tag 1304).</summary>
		FIX_StrikeExerciseStyle = 1304,

		/// <summary>FIX SecondaryPriceLimitType tag (tag 1305).</summary>
		FIX_SecondaryPriceLimitType = 1305,

		/// <summary>FIX PriceLimitType tag (tag 1306).</summary>
		FIX_PriceLimitType = 1306,

		/// <summary>FIX ExecInstValue tag (tag 1308).</summary>
		FIX_ExecInstValue = 1308,

		/// <summary>FIX NoTradingSessionRules tag (tag 1309).</summary>
		FIX_NoTradingSessionRules = 1309,

		/// <summary>FIX NoMarketSegments tag (tag 1310).</summary>
		FIX_NoMarketSegments = 1310,

		/// <summary>FIX NoDerivativeInstrAttrib tag (tag 1311).</summary>
		FIX_NoDerivativeInstrAttrib = 1311,

		/// <summary>FIX NoNestedInstrAttrib tag (tag 1312).</summary>
		FIX_NoNestedInstrAttrib = 1312,

		/// <summary>FIX DerivativeInstrAttribType tag (tag 1313).</summary>
		FIX_DerivativeInstrAttribType = 1313,

		/// <summary>FIX DerivativeInstrAttribValue tag (tag 1314).</summary>
		FIX_DerivativeInstrAttribValue = 1314,

		/// <summary>FIX DerivativePriceUnitOfMeasure tag (tag 1315).</summary>
		FIX_DerivativePriceUnitOfMeasure = 1315,

		/// <summary>FIX DerivativePriceUnitOfMeasureQty tag (tag 1316).</summary>
		FIX_DerivativePriceUnitOfMeasureQty = 1316,

		/// <summary>FIX DerivativeSettlMethod tag (tag 1317).</summary>
		FIX_DerivativeSettlMethod = 1317,

		/// <summary>FIX DerivativePriceQuoteMethod tag (tag 1318).</summary>
		FIX_DerivativePriceQuoteMethod = 1318,

		/// <summary>FIX DerivativeValuationMethod tag (tag 1319).</summary>
		FIX_DerivativeValuationMethod = 1319,

		/// <summary>FIX DerivativeListMethod tag (tag 1320).</summary>
		FIX_DerivativeListMethod = 1320,

		/// <summary>FIX DerivativeCapPrice tag (tag 1321).</summary>
		FIX_DerivativeCapPrice = 1321,

		/// <summary>FIX DerivativeFloorPrice tag (tag 1322).</summary>
		FIX_DerivativeFloorPrice = 1322,

		/// <summary>FIX DerivativePutOrCall tag (tag 1323).</summary>
		FIX_DerivativePutOrCall = 1323,

		/// <summary>FIX ListUpdateAction tag (tag 1324).</summary>
		FIX_ListUpdateAction = 1324,

		/// <summary>FIX LegPutOrCall tag (tag 1358).</summary>
		FIX_LegPutOrCall = 1358,

		/// <summary>FIX LegUnitOfMeasureQty tag (tag 1224).</summary>
		FIX_LegUnitOfMeasureQty = 1224,

		/// <summary>FIX LegPriceUnitOfMeasure tag (tag 1421).</summary>
		FIX_LegPriceUnitOfMeasure = 1421,

		/// <summary>FIX LegPriceUnitOfMeasureQty tag (tag 1422).</summary>
		FIX_LegPriceUnitOfMeasureQty = 1422,

		/// <summary>FIX UnderlyingUnitOfMeasureQty tag (tag 1423).</summary>
		FIX_UnderlyingUnitOfMeasureQty = 1423,

		/// <summary>FIX UnderlyingPriceUnitOfMeasure tag (tag 1424).</summary>
		FIX_UnderlyingPriceUnitOfMeasure = 1424,

		/// <summary>FIX UnderlyingPriceUnitOfMeasureQty tag (tag 1425).</summary>
		FIX_UnderlyingPriceUnitOfMeasureQty = 1425,

		/// <summary>FIX MarketReqID tag (tag 1393).</summary>
		FIX_MarketReqID = 1393,

		/// <summary>FIX MarketReportID tag (tag 1394).</summary>
		FIX_MarketReportID = 1394,

		/// <summary>FIX MarketUpdateAction tag (tag 1395).</summary>
		FIX_MarketUpdateAction = 1395,

		/// <summary>FIX MarketSegmentDesc tag (tag 1396).</summary>
		FIX_MarketSegmentDesc = 1396,

		/// <summary>FIX EncodedMktSegmDescLen tag (tag 1397).</summary>
		FIX_EncodedMktSegmDescLen = 1397,

		/// <summary>FIX EncodedMktSegmDesc tag (tag 1398).</summary>
		FIX_EncodedMktSegmDesc = 1398,

		/// <summary>FIX ParentMktSegmID tag (tag 1325).</summary>
		FIX_ParentMktSegmID = 1325,

		/// <summary>FIX TradingSessionDesc tag (tag 1326).</summary>
		FIX_TradingSessionDesc = 1326,

		/// <summary>FIX TradSesUpdateAction tag (tag 1327).</summary>
		FIX_TradSesUpdateAction = 1327,

		/// <summary>FIX RejectText tag (tag 1328).</summary>
		FIX_RejectText = 1328,

		/// <summary>FIX FeeMultiplier tag (tag 1329).</summary>
		FIX_FeeMultiplier = 1329,

		/// <summary>FIX UnderlyingLegSymbol tag (tag 1330).</summary>
		FIX_UnderlyingLegSymbol = 1330,

		/// <summary>FIX UnderlyingLegSymbolSfx tag (tag 1331).</summary>
		FIX_UnderlyingLegSymbolSfx = 1331,

		/// <summary>FIX UnderlyingLegSecurityID tag (tag 1332).</summary>
		FIX_UnderlyingLegSecurityID = 1332,

		/// <summary>FIX UnderlyingLegSecurityIDSource tag (tag 1333).</summary>
		FIX_UnderlyingLegSecurityIDSource = 1333,

		/// <summary>FIX NoUnderlyingLegSecurityAltID tag (tag 1334).</summary>
		FIX_NoUnderlyingLegSecurityAltID = 1334,

		/// <summary>FIX UnderlyingLegSecurityAltID tag (tag 1335).</summary>
		FIX_UnderlyingLegSecurityAltID = 1335,

		/// <summary>FIX UnderlyingLegSecurityAltIDSource tag (tag 1336).</summary>
		FIX_UnderlyingLegSecurityAltIDSource = 1336,

		/// <summary>FIX UnderlyingLegSecurityType tag (tag 1337).</summary>
		FIX_UnderlyingLegSecurityType = 1337,

		/// <summary>FIX UnderlyingLegSecuritySubType tag (tag 1338).</summary>
		FIX_UnderlyingLegSecuritySubType = 1338,

		/// <summary>FIX UnderlyingLegMaturityMonthYear tag (tag 1339).</summary>
		FIX_UnderlyingLegMaturityMonthYear = 1339,

		/// <summary>FIX UnderlyingLegPutOrCall tag (tag 1343).</summary>
		FIX_UnderlyingLegPutOrCall = 1343,

		/// <summary>FIX UnderlyingLegStrikePrice tag (tag 1340).</summary>
		FIX_UnderlyingLegStrikePrice = 1340,

		/// <summary>FIX UnderlyingLegSecurityExchange tag (tag 1341).</summary>
		FIX_UnderlyingLegSecurityExchange = 1341,

		/// <summary>FIX NoOfLegUnderlyings tag (tag 1342).</summary>
		FIX_NoOfLegUnderlyings = 1342,

		/// <summary>FIX UnderlyingLegCFICode tag (tag 1344).</summary>
		FIX_UnderlyingLegCFICode = 1344,

		/// <summary>FIX UnderlyingLegMaturityDate tag (tag 1345).</summary>
		FIX_UnderlyingLegMaturityDate = 1345,

		/// <summary>FIX UnderlyingLegMaturityTime tag (tag 1405).</summary>
		FIX_UnderlyingLegMaturityTime = 1405,

		/// <summary>FIX UnderlyingLegOptAttribute tag (tag 1391).</summary>
		FIX_UnderlyingLegOptAttribute = 1391,

		/// <summary>FIX UnderlyingLegSecurityDesc tag (tag 1392).</summary>
		FIX_UnderlyingLegSecurityDesc = 1392,

		/// <summary>FIX EncryptedPasswordMethod tag (tag 1400).</summary>
		FIX_EncryptedPasswordMethod = 1400,

		/// <summary>FIX EncryptedPasswordLen tag (tag 1401).</summary>
		FIX_EncryptedPasswordLen = 1401,

		/// <summary>FIX EncryptedPassword tag (tag 1402).</summary>
		FIX_EncryptedPassword = 1402,

		/// <summary>FIX EncryptedNewPasswordLen tag (tag 1403).</summary>
		FIX_EncryptedNewPasswordLen = 1403,

		/// <summary>FIX EncryptedNewPassword tag (tag 1404).</summary>
		FIX_EncryptedNewPassword = 1404,

		/// <summary>FIX ApplExtID tag (tag 1156).</summary>
		FIX_ApplExtID = 1156,

		/// <summary>FIX RefApplExtID tag (tag 1406).</summary>
		FIX_RefApplExtID = 1406,

		/// <summary>FIX DefaultApplExtID tag (tag 1407).</summary>
		FIX_DefaultApplExtID = 1407,

		/// <summary>FIX DefaultCstmApplVerID tag (tag 1408).</summary>
		FIX_DefaultCstmApplVerID = 1408,

		/// <summary>FIX SessionStatus tag (tag 1409).</summary>
		FIX_SessionStatus = 1409,

		/// <summary>FIX DefaultVerIndicator tag (tag 1410).</summary>
		FIX_DefaultVerIndicator = 1410,

		/// <summary>FIX NoUsernames tag (tag 809).</summary>
		FIX_NoUsernames = 809,

		/// <summary>FIX LegAllocSettlCurrency tag (tag 1367).</summary>
		FIX_LegAllocSettlCurrency = 1367,

		/// <summary>FIX TotNoFills tag (tag 1361).</summary>
		FIX_TotNoFills = 1361,

		/// <summary>FIX NoFills tag (tag 1362).</summary>
		FIX_NoFills = 1362,

		/// <summary>FIX FillExecID tag (tag 1363).</summary>
		FIX_FillExecID = 1363,

		/// <summary>FIX FillPx tag (tag 1364).</summary>
		FIX_FillPx = 1364,

		/// <summary>FIX FillQty tag (tag 1365).</summary>
		FIX_FillQty = 1365,

		/// <summary>FIX LegAllocID tag (tag 1366).</summary>
		FIX_LegAllocID = 1366,

		/// <summary>FIX TradSesEvent tag (tag 1368).</summary>
		FIX_TradSesEvent = 1368,

		/// <summary>FIX MassActionReportID tag (tag 1369).</summary>
		FIX_MassActionReportID = 1369,

		/// <summary>FIX NoNotAffectedOrders tag (tag 1370).</summary>
		FIX_NoNotAffectedOrders = 1370,

		/// <summary>FIX NotAffectedOrderID tag (tag 1371).</summary>
		FIX_NotAffectedOrderID = 1371,

		/// <summary>FIX NotAffOrigClOrdID tag (tag 1372).</summary>
		FIX_NotAffOrigClOrdID = 1372,

		/// <summary>FIX MassActionType tag (tag 1373).</summary>
		FIX_MassActionType = 1373,

		/// <summary>FIX MassActionScope tag (tag 1374).</summary>
		FIX_MassActionScope = 1374,

		/// <summary>FIX MassActionResponse tag (tag 1375).</summary>
		FIX_MassActionResponse = 1375,

		/// <summary>FIX MassActionRejectReason tag (tag 1376).</summary>
		FIX_MassActionRejectReason = 1376,

		/// <summary>FIX MultilegModel tag (tag 1377).</summary>
		FIX_MultilegModel = 1377,

		/// <summary>FIX MultilegPriceMethod tag (tag 1378).</summary>
		FIX_MultilegPriceMethod = 1378,

		/// <summary>FIX LegVolatility tag (tag 1379).</summary>
		FIX_LegVolatility = 1379,

		/// <summary>FIX DividendYield tag (tag 1380).</summary>
		FIX_DividendYield = 1380,

		/// <summary>FIX LegDividendYield tag (tag 1381).</summary>
		FIX_LegDividendYield = 1381,

		/// <summary>FIX CurrencyRatio tag (tag 1382).</summary>
		FIX_CurrencyRatio = 1382,

		/// <summary>FIX LegCurrencyRatio tag (tag 1383).</summary>
		FIX_LegCurrencyRatio = 1383,

		/// <summary>FIX LegExecInst tag (tag 1384).</summary>
		FIX_LegExecInst = 1384,

		/// <summary>FIX ContingencyType tag (tag 1385).</summary>
		FIX_ContingencyType = 1385,

		/// <summary>FIX ListRejectReason tag (tag 1386).</summary>
		FIX_ListRejectReason = 1386,

		/// <summary>FIX NoTrdRepIndicators tag (tag 1387).</summary>
		FIX_NoTrdRepIndicators = 1387,

		/// <summary>FIX TrdRepPartyRole tag (tag 1388).</summary>
		FIX_TrdRepPartyRole = 1388,

		/// <summary>FIX TrdRepIndicator tag (tag 1389).</summary>
		FIX_TrdRepIndicator = 1389,

		/// <summary>FIX TradePublishIndicator tag (tag 1390).</summary>
		FIX_TradePublishIndicator = 1390,

		/// <summary>FIX ApplReqID tag (tag 1346).</summary>
		FIX_ApplReqID = 1346,

		/// <summary>FIX ApplReqType tag (tag 1347).</summary>
		FIX_ApplReqType = 1347,

		/// <summary>FIX ApplResponseType tag (tag 1348).</summary>
		FIX_ApplResponseType = 1348,

		/// <summary>FIX ApplTotalMessageCount tag (tag 1349).</summary>
		FIX_ApplTotalMessageCount = 1349,

		/// <summary>FIX ApplLastSeqNum tag (tag 1350).</summary>
		FIX_ApplLastSeqNum = 1350,

		/// <summary>FIX NoApplIDs tag (tag 1351).</summary>
		FIX_NoApplIDs = 1351,

		/// <summary>FIX ApplResendFlag tag (tag 1352).</summary>
		FIX_ApplResendFlag = 1352,

		/// <summary>FIX ApplResponseID tag (tag 1353).</summary>
		FIX_ApplResponseID = 1353,

		/// <summary>FIX ApplResponseError tag (tag 1354).</summary>
		FIX_ApplResponseError = 1354,

		/// <summary>FIX RefApplID tag (tag 1355).</summary>
		FIX_RefApplID = 1355,

		/// <summary>FIX ApplReportID tag (tag 1356).</summary>
		FIX_ApplReportID = 1356,

		/// <summary>FIX RefApplLastSeqNum tag (tag 1357).</summary>
		FIX_RefApplLastSeqNum = 1357,

		/// <summary>FIX ApplNewSeqNum tag (tag 1399).</summary>
		FIX_ApplNewSeqNum = 1399,

		/// <summary>FIX ApplReportType tag (tag 1426).</summary>
		FIX_ApplReportType = 1426,

		/// <summary>FIX Nested4PartySubIDType tag (tag 1411).</summary>
		FIX_Nested4PartySubIDType = 1411,

		/// <summary>FIX Nested4PartySubID tag (tag 1412).</summary>
		FIX_Nested4PartySubID = 1412,

		/// <summary>FIX NoNested4PartySubIDs tag (tag 1413).</summary>
		FIX_NoNested4PartySubIDs = 1413,

		/// <summary>FIX NoNested4PartyIDs tag (tag 1414).</summary>
		FIX_NoNested4PartyIDs = 1414,

		/// <summary>FIX Nested4PartyID tag (tag 1415).</summary>
		FIX_Nested4PartyID = 1415,

		/// <summary>FIX Nested4PartyIDSource tag (tag 1416).</summary>
		FIX_Nested4PartyIDSource = 1416,

		/// <summary>FIX Nested4PartyRole tag (tag 1417).</summary>
		FIX_Nested4PartyRole = 1417,

		/// <summary>FIX LegLastQty tag (tag 1418).</summary>
		FIX_LegLastQty = 1418,

		/// <summary>FIX SideExecID tag (tag 1427).</summary>
		FIX_SideExecID = 1427,

		/// <summary>FIX OrderDelay tag (tag 1428).</summary>
		FIX_OrderDelay = 1428,

		/// <summary>FIX OrderDelayUnit tag (tag 1429).</summary>
		FIX_OrderDelayUnit = 1429,

		/// <summary>FIX VenueType tag (tag 1430).</summary>
		FIX_VenueType = 1430,

		/// <summary>FIX RefOrdIDReason tag (tag 1431).</summary>
		FIX_RefOrdIDReason = 1431,

		/// <summary>FIX OrigCustOrderCapacity tag (tag 1432).</summary>
		FIX_OrigCustOrderCapacity = 1432,

		/// <summary>FIX RefApplReqID tag (tag 1433).</summary>
		FIX_RefApplReqID = 1433,

		/// <summary>FIX ModelType tag (tag 1434).</summary>
		FIX_ModelType = 1434,

		/// <summary>FIX ContractMultiplierUnit tag (tag 1435).</summary>
		FIX_ContractMultiplierUnit = 1435,

		/// <summary>FIX LegContractMultiplierUnit tag (tag 1436).</summary>
		FIX_LegContractMultiplierUnit = 1436,

		/// <summary>FIX UnderlyingContractMultiplierUnit tag (tag 1437).</summary>
		FIX_UnderlyingContractMultiplierUnit = 1437,

		/// <summary>FIX DerivativeContractMultiplierUnit tag (tag 1438).</summary>
		FIX_DerivativeContractMultiplierUnit = 1438,

		/// <summary>FIX FlowScheduleType tag (tag 1439).</summary>
		FIX_FlowScheduleType = 1439,

		/// <summary>FIX LegFlowScheduleType tag (tag 1440).</summary>
		FIX_LegFlowScheduleType = 1440,

		/// <summary>FIX UnderlyingFlowScheduleType tag (tag 1441).</summary>
		FIX_UnderlyingFlowScheduleType = 1441,

		/// <summary>FIX DerivativeFlowScheduleType tag (tag 1442).</summary>
		FIX_DerivativeFlowScheduleType = 1442,

		/// <summary>FIX FillLiquidityInd tag (tag 1443).</summary>
		FIX_FillLiquidityInd = 1443,

		/// <summary>FIX SideLiquidityInd tag (tag 1444).</summary>
		FIX_SideLiquidityInd = 1444,

		/// <summary>FIX NoRateSources tag (tag 1445).</summary>
		FIX_NoRateSources = 1445,

		/// <summary>FIX RateSource tag (tag 1446).</summary>
		FIX_RateSource = 1446,

		/// <summary>FIX RateSourceType tag (tag 1447).</summary>
		FIX_RateSourceType = 1447,

		/// <summary>FIX ReferencePage tag (tag 1448).</summary>
		FIX_ReferencePage = 1448,

		/// <summary>FIX RestructuringType tag (tag 1449).</summary>
		FIX_RestructuringType = 1449,

		/// <summary>FIX Seniority tag (tag 1450).</summary>
		FIX_Seniority = 1450,

		/// <summary>FIX NotionalPercentageOutstanding tag (tag 1451).</summary>
		FIX_NotionalPercentageOutstanding = 1451,

		/// <summary>FIX OriginalNotionalPercentageOutstanding tag (tag 1452).</summary>
		FIX_OriginalNotionalPercentageOutstanding = 1452,

		/// <summary>FIX UnderlyingRestructuringType tag (tag 1453).</summary>
		FIX_UnderlyingRestructuringType = 1453,

		/// <summary>FIX UnderlyingSeniority tag (tag 1454).</summary>
		FIX_UnderlyingSeniority = 1454,

		/// <summary>FIX UnderlyingNotionalPercentageOutstanding tag (tag 1455).</summary>
		FIX_UnderlyingNotionalPercentageOutstanding = 1455,

		/// <summary>FIX UnderlyingOriginalNotionalPercentageOutstanding tag (tag 1456).</summary>
		FIX_UnderlyingOriginalNotionalPercentageOutstanding = 1456,

		/// <summary>FIX AttachmentPoint tag (tag 1457).</summary>
		FIX_AttachmentPoint = 1457,

		/// <summary>FIX DetachmentPoint tag (tag 1458).</summary>
		FIX_DetachmentPoint = 1458,

		/// <summary>FIX UnderlyingAttachmentPoint tag (tag 1459).</summary>
		FIX_UnderlyingAttachmentPoint = 1459,

		/// <summary>FIX UnderlyingDetachmentPoint tag (tag 1460).</summary>
		FIX_UnderlyingDetachmentPoint = 1460,

		/// <summary>FIX NoTargetPartyIDs tag (tag 1461).</summary>
		FIX_NoTargetPartyIDs = 1461,

		/// <summary>FIX TargetPartyID tag (tag 1462).</summary>
		FIX_TargetPartyID = 1462,

		/// <summary>FIX TargetPartyIDSource tag (tag 1463).</summary>
		FIX_TargetPartyIDSource = 1463,

		/// <summary>FIX TargetPartyRole tag (tag 1464).</summary>
		FIX_TargetPartyRole = 1464,

		/// <summary>FIX SecurityListID tag (tag 1465).</summary>
		FIX_SecurityListID = 1465,

		/// <summary>FIX SecurityListRefID tag (tag 1466).</summary>
		FIX_SecurityListRefID = 1466,

		/// <summary>FIX SecurityListDesc tag (tag 1467).</summary>
		FIX_SecurityListDesc = 1467,

		/// <summary>FIX EncodedSecurityListDescLen tag (tag 1468).</summary>
		FIX_EncodedSecurityListDescLen = 1468,

		/// <summary>FIX EncodedSecurityListDesc tag (tag 1469).</summary>
		FIX_EncodedSecurityListDesc = 1469,

		/// <summary>FIX SecurityListType tag (tag 1470).</summary>
		FIX_SecurityListType = 1470,

		/// <summary>FIX SecurityListTypeSource tag (tag 1471).</summary>
		FIX_SecurityListTypeSource = 1471,

		/// <summary>FIX NewsID tag (tag 1472).</summary>
		FIX_NewsID = 1472,

		/// <summary>FIX NewsCategory tag (tag 1473).</summary>
		FIX_NewsCategory = 1473,

		/// <summary>FIX LanguageCode tag (tag 1474).</summary>
		FIX_LanguageCode = 1474,

		/// <summary>FIX NoNewsRefIDs tag (tag 1475).</summary>
		FIX_NoNewsRefIDs = 1475,

		/// <summary>FIX NewsRefID tag (tag 1476).</summary>
		FIX_NewsRefID = 1476,

		/// <summary>FIX NewsRefType tag (tag 1477).</summary>
		FIX_NewsRefType = 1477,

		/// <summary>FIX StrikePriceDeterminationMethod tag (tag 1478).</summary>
		FIX_StrikePriceDeterminationMethod = 1478,

		/// <summary>FIX StrikePriceBoundaryMethod tag (tag 1479).</summary>
		FIX_StrikePriceBoundaryMethod = 1479,

		/// <summary>FIX StrikePriceBoundaryPrecision tag (tag 1480).</summary>
		FIX_StrikePriceBoundaryPrecision = 1480,

		/// <summary>FIX UnderlyingPriceDeterminationMethod tag (tag 1481).</summary>
		FIX_UnderlyingPriceDeterminationMethod = 1481,

		/// <summary>FIX OptPayoutType tag (tag 1482).</summary>
		FIX_OptPayoutType = 1482,

		/// <summary>FIX NoComplexEvents tag (tag 1483).</summary>
		FIX_NoComplexEvents = 1483,

		/// <summary>FIX ComplexEventType tag (tag 1484).</summary>
		FIX_ComplexEventType = 1484,

		/// <summary>FIX ComplexOptPayoutAmount tag (tag 1485).</summary>
		FIX_ComplexOptPayoutAmount = 1485,

		/// <summary>FIX ComplexEventPrice tag (tag 1486).</summary>
		FIX_ComplexEventPrice = 1486,

		/// <summary>FIX ComplexEventPriceBoundaryMethod tag (tag 1487).</summary>
		FIX_ComplexEventPriceBoundaryMethod = 1487,

		/// <summary>FIX ComplexEventPriceBoundaryPrecision tag (tag 1488).</summary>
		FIX_ComplexEventPriceBoundaryPrecision = 1488,

		/// <summary>FIX ComplexEventPriceTimeType tag (tag 1489).</summary>
		FIX_ComplexEventPriceTimeType = 1489,

		/// <summary>FIX ComplexEventCondition tag (tag 1490).</summary>
		FIX_ComplexEventCondition = 1490,

		/// <summary>FIX NoComplexEventDates tag (tag 1491).</summary>
		FIX_NoComplexEventDates = 1491,

		/// <summary>FIX ComplexEventStartDate tag (tag 1492).</summary>
		FIX_ComplexEventStartDate = 1492,

		/// <summary>FIX ComplexEventEndDate tag (tag 1493).</summary>
		FIX_ComplexEventEndDate = 1493,

		/// <summary>FIX NoComplexEventTimes tag (tag 1494).</summary>
		FIX_NoComplexEventTimes = 1494,

		/// <summary>FIX ComplexEventStartTime tag (tag 1495).</summary>
		FIX_ComplexEventStartTime = 1495,

		/// <summary>FIX ComplexEventEndTime tag (tag 1496).</summary>
		FIX_ComplexEventEndTime = 1496,

		/// <summary>FIX StreamAsgnReqID tag (tag 1497).</summary>
		FIX_StreamAsgnReqID = 1497,

		/// <summary>FIX StreamAsgnReqType tag (tag 1498).</summary>
		FIX_StreamAsgnReqType = 1498,

		/// <summary>FIX NoAsgnReqs tag (tag 1499).</summary>
		FIX_NoAsgnReqs = 1499,

		/// <summary>FIX MDStreamID tag (tag 1500).</summary>
		FIX_MDStreamID = 1500,

		/// <summary>FIX StreamAsgnRptID tag (tag 1501).</summary>
		FIX_StreamAsgnRptID = 1501,

		/// <summary>FIX StreamAsgnRejReason tag (tag 1502).</summary>
		FIX_StreamAsgnRejReason = 1502,

		/// <summary>FIX StreamAsgnAckType tag (tag 1503).</summary>
		FIX_StreamAsgnAckType = 1503,

		/// <summary>FIX StreamAsgnType tag (tag 1617).</summary>
		FIX_StreamAsgnType = 1617,

		/// <summary>FIX RelSymTransactTime tag (tag 1504).</summary>
		FIX_RelSymTransactTime = 1504

	}
}
