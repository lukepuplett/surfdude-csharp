# Surfdude

Because you can surf the best APIs.

A simple little hypermedia client that dynamically drives a REST API. You can use this to test that an API is genuinely
RESTful, i.e. it can be driven entirely by links and hypertext transfer, or you can use this in your application to work
a backend API.

Designing APIs by writing the desired client code first tends to drive out much nicer APIs.

## API UX and Interaction Journeys

The premise is that, like a website, a well designed API is centred around the client's needs and problems 
much like Design Thinking for the websites of the world.

This ultimately leads to thinking of the things that a client would like to do and their journey through the API to get
those things done. At each stage of the journey there is opportunity to feedback and inform the client of the impact of
any changes and to present alternative options and possible next interactions.

## Sample

Surfdude makes this kind of code possible. Notice how the protocol methods and resource identifiers are completely absent,
reinforcing the notion that URLs cannot themselves be RESTful; a machine cares not for pluralised nouns and semantic
structure.

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

## Bring Your Own Media-Type

As Roy Fielding explained, most of the effort in designing a REST API should go into designing the media-type.

The JSON, HTML or XML of your API should contain hypermedia controls that contain all the information needed for the client
to make use of those controls. For HTML the `<a>` and `<form>` tags come with clear processing instructions for web browser
clients to make use of these controls whenever they are encountered.

The upshot of this uniformity is that a nice website needs no further documentation; if the browser cannot work with the
HTML then there is a bug. If a website needs a user manual then there is a serious UX issue. The same is true of an API.

Surfude lets you replace the classes that read and process your hypertext resources and their controls.

## Evolving APIs

Websites change over time and yet customers rarely notice. Their experience remains uninterrupted, the links keep working
and adding an item to a shopping cart continues to work, if though the underlying resources are changing all day.

A great API using a dynamic client affords the same flexibility since interactions are loosely based on relation names.
