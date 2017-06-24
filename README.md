### The solution was developed in Visual Studio 2012

## Briefly describe the conceptual approach you chose! What are the trade-offs?
1. The responsibility of rule engine is to apply list of validations. So I used [chain of responsibility pattern](https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern) to solve this. Each unit in the chain is responsible for validating a particular rule. The design allows addition of new validations easily without modifying existing validations thus reducing the amount of regression needed.
2. I have used [notification pattern](https://martinfowler.com/articles/replaceThrowWithNotification.html) to capture the validations. In the future, observer pattern can be used to subscribe to the validations as and when they are recorded.
3. The current design cannot be used to parallelise the validations. It passes the notification instance to capture the errors. In theory, each row in the incoming data can be run through the validations parallely. But the current solution doesn't support this.
4. The current design assumes the entire data is in the memory. This might not work if the data is huge and its streamed. We will have to modify the design little bit to handle streaming data.

## What's the runtime performance? What is the complexity? Where are the bottlenecks?
The run time complexity of the algorithm is O(N) where N is the number of rows(data units) in the input data stream. The points (3 and 4) above explains the bottlenecks.

## If you had more time, what improvements would you make, and in what order of priority?

1. Add support for running validations parallely in the incoming data stream. We can achieve this if validators return error messages instead of an instance of notification which is shared across validators.
2. Add support for incoming data stream instead of assuming entire data is present in memory
3. Improve rule parsing logic to add support for negation. eg - Current solution doesn't support if a rule is added as - 'ATL3 should be in future'
