# Surfdude

A simple little hypermedia client that dynamically drives a REST API. You can use this to test that an API is genuinely
RESTful, i.e. it can be driven entirely by links and hypertext transfer, or you can use this in your application to work
a backend API.

Designing APIs by writing the desired client code first tends to drive out much nicer APIs.

## API UX and Interaction Journeys

The premise is that, like a website, a well designed REST API is centred around the client's needs and problems 
much like Design Thinking for the websites of the world.

This ultimately leads to thinking of the things that a
client would like to do and their journey through the API to get those things done. At each stage of the journey
there is opportunity to feedback and inform the client of the impact of any changes and to present alternative
options and possible next interactions.

## Sample

Surfdude makes this kind of code possible.

	var report = await Journey.Start("https://private-ac89c-surfdude.apiary-mock.com/")
		.FromRoot()
		.Request("registrations")
		.RequestItem(0)
		.Submit("update-contact-details", new { email = "mike@beans.com" })
		.Read(out Func<ResourceModel> getContactDetails)
		.Request("registration")
		.Submit("add-processing-instruction", new { message = "The applicant is partially sighted." })
		.RunAsync();

	Console.WriteLine(getContactDetails().Email);
