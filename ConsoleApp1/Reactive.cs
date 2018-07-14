using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxTestApp {

    class Reactive {

        public Reactive() { }

        public void AsyncSubjectTest() {
            //todo
        }

        // The correct way to handle exceptions is to provide a delegate for OnError notifications as in this example.
        public void SubscriptionTest() {
            var values = new Subject<int>();
            values.Subscribe(
                value => Console.WriteLine("1st subscription received {0}", value),
                ex => Console.WriteLine("Caught an exception : {0}", ex));
            values.OnNext(0);
            values.OnError(new Exception("Dummy exception!"));
        }

        // Dispose() disposes of subscription previously called on a given observable. Any further item(s) emitted to the Observer (with OnNext() or OnComplete()) results in nothing because subscription has been disposed.
        public void UnsubscribeTest() {
            var values = new Subject<int>();
            var firstSubscription = values.Subscribe(value =>
                Console.WriteLine("1st subscription received {0}", value)
            );
            var secondSubscription = values.Subscribe(value =>
                Console.WriteLine("2nd subscription received {0}", value)
            );
            values.OnNext(0);
            values.OnNext(1);
            values.OnNext(2);
            values.OnNext(3);
            firstSubscription.Dispose();
            Console.WriteLine("Disposed of 1st subscription");
            values.OnNext(4);
            values.OnNext(5);
        }

        // When OnError and OnCompleted are implemented in a subscription, they will get called in a proper way.
        // E.g: When an exception is thrown in OnError, the given implementation will handle it inside onError action.
        // If the Observable source gets an OnCompleted signal, it will prevent any onNext action handling.
        public void OnErrorAndOnCompletedTest() {
            var subject = new Subject<int>();
            subject.Subscribe(
                value => Console.WriteLine(value),
                ex => Console.WriteLine("OnError called {0}", ex),
                () => Console.WriteLine("OnCompleted called")
            );
            subject.OnError(new Exception("Thrown a new exception"));
            subject.OnCompleted();
            subject.OnNext(2);
            Console.WriteLine("I'm free from exceptions!");
            subject.Dispose(); // An interesting thing to consider is that when a sequence completes or errors, you should still dispose of your subscription.
        }

        // The Create method will ensure the standard Dispose semantics, so calling Dispose() multiple times will only invoke the delegate you provide once.
        public void DisposeTest() {
            var disposable = Disposable.Create(() => Console.WriteLine("Being disposed."));
            Console.WriteLine("Calling dispose...");
            disposable.Dispose();
            Console.WriteLine("Calling again...");
            disposable.Dispose();
        }

        // Observable.Return() is the same as Observable.just() in Java. It emits an item then sends az OnCompleted() signal immediately.
        public void ObservableReturnTest() {
            var singleValue = Observable.Return("Value");
            //which could have also been simulated with a replay subject
            var subject = new ReplaySubject<string>();
            subject.OnNext("Value");
            subject.OnCompleted();
        }
    }
}
