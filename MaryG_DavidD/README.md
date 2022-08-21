# Joint Presentation at [Code PaLOUsa 2022](https://www.codepalousa.com/)

## _Speakers_: **[Mary Grygleski](https://twitter.com/mgrygles) and [David Dieruf](https://twitter.com/DierufDavid)**

## _Title_: **Event Messaging and Streaming with [Apache Pulsar](https://pulsar.apache.org)**

When it comes to distributed, event-driven messaging systems, we usually see them supporting either one of two types of semantics: streaming, or queueing, and rarely do we find a platform that supports both. In this presentation, we’ll first get an introduction and some clarifications of event-driven versus message-driven systems, event streams, and stream processing. We’ll then take a look at Apache Pulsar which offers a very unique capability in modern, cloud-native applications and architecture, in which its platform supports both Pub-Sub and Message Queues, and extends into streams processing as well as performs message mediation & transformation. We will look at how it relies on Apache Bookkeeper for its durable, scalable, and performant storage of log streams, and leverages on Apache Zookeeper. We will also see how Pulsar is meant to bring the best of other systems, such as how it fills the gaps that Kafka has and extends its streaming capability in the complex cloud world.


