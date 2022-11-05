﻿using Dashboard;
using Dashboard.Server.Persistence;
using PlexShareDashboard.Dashboard.Server.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace PlexShareTests.DashboardTests.Telemetry
{
    public class TelemetryTests
    {
        //in  this we write the unit test for the telemetry submodule 
        //defining  the persistence factory for this purpose 
        private readonly TelemetryPersistence persistenceInstance = new TelemetryPersistence();

        //checking the test case for singleton design pattern 
        [Fact]
        public void SingletonFactory_Test_Check()
        {
            ITelemetry telemetryInstance1 = TelemetryFactory.GetTelemetryInstance();
            ITelemetry telemetryInstance2 = TelemetryFactory.GetTelemetryInstance();

            Assert.Equal(telemetryInstance1, telemetryInstance2);
            //Assert.True(telemetryInstance1.Equals(telemetryInstance2));
        
        }

        [Fact]
        //writing the first test to checking the function CalculateUserCountVsTimeStamp 
        public void CalculateUserCountVsTimeStamp_ShouldGiveUserCountAtTimeStamp_Simple_Test()
        { 
            //Arrange 
            //first we have to arrange or do the setup 
            UserData user1 = new UserData("Rupesh", 1);

            //defining the new session 
            SessionData sessionData1 = new SessionData();
            //adding the new user to the session data to hardcode the users list 
            sessionData1.AddUser(user1);

            //ACT ==> now we have to bring the telemetry module in action to check whether it is working fine or not

            DateTime currDateTime = DateTime.Now;
            //defining the instance for the telemetry 
            var telemetryInstance = TelemetryFactory.GetTelemetryInstance();

            //calling the function to find the user count vs the time stamp 
            telemetryInstance.CalculateUserCountVsTimeStamp(sessionData1, currDateTime);
            int result1 = telemetryInstance.userCountVsEachTimeStamp[currDateTime];

            //ASSERT ==> now we have to check whether the output is the correct or not 
            var check1 = false;
            int userCount = sessionData1.users.Count();
            if (result1 == userCount)
            {
                check1 = true;
            }

            Assert.True(check1);

            //say everything went fine 
            return;


        }
        [Fact]
        //Complex test case to check the calculate user count vs time stamp function 
        public void CalculateUserCountVsTimeStamp_ShouldGiveUserCountAtTimeStamp_Complex_Test()
        {
            //Arrange ==> defining the users 
            UserData user1 = new UserData("Rupesh Kumar", 1);
            UserData user2 = new UserData("Shubham Raj", 2);
            UserData user3 = new UserData("Saurabh Kumar", 3);
            UserData user4 = new UserData("Aditya Agarwal", 4);
            UserData user5 = new UserData("Hrishi Raaj", 5);
            UserData user6 = new UserData("user6", 6);
            UserData user7 = new UserData("user7", 7);
            UserData user8 = new UserData("user8", 8);

            //defining the session data 
            SessionData sessionData = new SessionData();
            //var currTime = new DateTime(2021, 11, 23, 1, 0, 0);
            DateTime currDateTime1 = new DateTime(2021, 11, 23, 1, 0, 0);
            DateTime currDateTime2 = new DateTime(2021, 11, 23, 1, 1, 0);
            DateTime currDateTime3 = new DateTime(2021, 11, 23, 1, 2, 0);

            sessionData.AddUser(user1);
            sessionData.AddUser(user2);
            sessionData.AddUser(user3);

            var telemetryInstance = TelemetryFactory.GetTelemetryInstance();
            telemetryInstance.CalculateUserCountVsTimeStamp(sessionData, currDateTime1);
            int result1 = telemetryInstance.userCountVsEachTimeStamp[currDateTime1];

            sessionData.AddUser(user4);
            sessionData.AddUser(user5);

            telemetryInstance.CalculateUserCountVsTimeStamp(sessionData, currDateTime2);
            int result2 = telemetryInstance.userCountVsEachTimeStamp[currDateTime2];


            sessionData.AddUser(user6);
            sessionData.AddUser(user7);
            sessionData.AddUser(user8);

            telemetryInstance.CalculateUserCountVsTimeStamp(sessionData, currDateTime3);
            int result3 = telemetryInstance.userCountVsEachTimeStamp[currDateTime3];
            
            
            bool check1 = false;
            bool check2 = false;
            bool check3 = false;

            if (result1 == 3)
            {
                check1 = true;
            }

            if (result2 == 5)
            {
                check2 = true;
            }

            if (result3 == 8)
            {
                check3 = true;
            }

            Assert.True(check1 && check2 && check3);

            //say everything went fine 
            return;
        }




        //Testing for calculation of entry time of the users 
        public void CalculateArrivalExitTimeOfUser_Test_Entry_Time_Calculation()
        { 
            //Arrange ==> this 
        }
    }
}
