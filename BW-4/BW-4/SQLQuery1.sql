
CREATE TABLE [dbo].[Carrello] (
    [IdCarrello]    INT   IDENTITY (1, 1) NOT NULL,
    [PrezzoTotale] MONEY DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCarrello] ASC)
);




CREATE TABLE [dbo].[Generi] (
    [IdGenere]     INT           IDENTITY (1, 1) NOT NULL,
    [TipoDiGenere] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdGenere] ASC)
);



CREATE TABLE [dbo].[Immagini] (
    [IdImmagini]          INT            IDENTITY (1, 1) NOT NULL,
    [ImmagineCopertina]   NVARCHAR (MAX) NOT NULL,
    [ImmagineSecondaria1] NVARCHAR (MAX) NOT NULL,
    [ImmagineSecondaria2] NVARCHAR (MAX) NOT NULL,
    [ImmagineSecondaria3] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdImmagini] ASC)
);






CREATE TABLE [dbo].[Piattaforme] (
    [IdPiattaforma]   INT           IDENTITY (1, 1) NOT NULL,
    [NomePiattaforma] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPiattaforma] ASC)
);










