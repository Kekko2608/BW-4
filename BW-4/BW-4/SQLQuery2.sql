CREATE TABLE [dbo].[Prodotti] (
    [IdProdotto]          INT            IDENTITY (1, 1) NOT NULL,
    [NomeProdotto]        NVARCHAR (60)  NOT NULL,
    [DescrizioneProdotto] NVARCHAR (200) NOT NULL,
    [Brand]               NVARCHAR (60)  NOT NULL,
    [PEGI]                CHAR (2)       NOT NULL,
    [CodiceABarre]        CHAR (13)      NOT NULL,
    [Disponibilita]       BIT            NOT NULL,
    [Prezzo]              MONEY          NOT NULL,
    [IdPiattaforma]       INT            NOT NULL,
    [IdGenere]            INT            NOT NULL,
    [IdImmagini]          INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProdotto] ASC),
    CONSTRAINT [FK_Prodotti_Genere] FOREIGN KEY ([IdGenere]) REFERENCES [dbo].[Generi] ([IdGenere]),
    CONSTRAINT [FK_Prodotti_Piattafome] FOREIGN KEY ([IdPiattaforma]) REFERENCES [dbo].[Piattaforme] ([IdPiattaforma]),
    CONSTRAINT [FK_Prodotti_Immagini] FOREIGN KEY ([IdImmagini]) REFERENCES [dbo].[Immagini] ([IdImmagini])
);

CREATE TABLE [dbo].[Ordini] (
    [IdOrdine]   INT IDENTITY (1, 1) NOT NULL,
    [IdProdotti] INT NOT NULL,
    [IdCarrello] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdOrdine] ASC),
    CONSTRAINT [FK_Ordini_Carrello] FOREIGN KEY ([IdCarrello]) REFERENCES [dbo].[Carrello] ([IdCarrello]),
    CONSTRAINT [FK_Ordini_Prodotti] FOREIGN KEY ([IdProdotti]) REFERENCES [dbo].[Prodotti] ([IdProdotto])
);
