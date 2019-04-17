# Surfdude

Because you can surf the best APIs.

A simple little hypermedia client that dynamically drives a REST API. You can use this to test that an API is genuinely
RESTful, i.e. it can be driven entirely by links and hypertext transfer, or you can use this in your application to work
a backend API.

**Designing APIs by writing the desired client code first tends to drive out much nicer APIs.** Surfdude will emit helpful errors that let you amend the API until each error is fixed and the entire wave can be ridden successfully. You could potentially even replace the step code with code that generates the JSON that would make the step work! ...woah.

## API UX and Riding Interaction Waves

The premise is that, like a website, a well designed API is centred around the client's needs and problems 
much like how web and interaction designers build the experiences we all enjoy on the web. Mostly.

For an API this means thinking of the things that a client would like to do with the API, and their journey through the API to get
those things done. At each step of the journey there is opportunity to feedback and inform the client of the impact of
any changes and to present alternative options and possible next interactions.

## Sample Ride

Surfdude lets you express an interaction with an API as a ride on a wave.

	SurfReport report = await Surf.Wave("https://private-ac89c-surfdude.apiary-mock.com/")
		.FromRoot()
		.To("registrations")
		.ToItem(0)
		.Submit("update-contact-details", new { email = "mike@beans.com" })
		.Read(out Func<ResourceModel> getContactDetails)
		.To("registration")
		.Submit("add-processing-instruction", new { message = "The applicant is partially sighted." })
		.RideItAsync();

	Console.WriteLine(getContactDetails().Email);

Notice how the protocol methods and resource identifiers are completely absent, reinforcing the notion that URLs cannot themselves be RESTful; a machine cares not for your wonderfully pluralised nouns and semantic structure, just as an online shopper cares not for the URL sitting beneath a button click.

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
and adding an item to a shopping cart continues to work, if though the underlying resources may be changing each day.

A great API using a dynamic client affords the same flexibility since interactions are loosely based on relation names.
