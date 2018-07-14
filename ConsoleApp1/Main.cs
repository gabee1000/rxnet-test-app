using System;
using System.Reactive.Subjects;
using RxTestApp;

namespace RxTestApp {

    class MainClass {

        static void Main(string[] args) {
            var reactive = new Reactive();
            reactive.AsyncSubjectTest();
            reactive.SubscriptionTest();
            Console.ReadKey();
        }
    }
}
