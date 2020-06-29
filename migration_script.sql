-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: parebemativos
-- Source Schemata: parebemativos
-- Created: Fri Jun 26 12:32:27 2020
-- Workbench Version: 8.0.17
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema parebemativos
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `parebemativos` ;
CREATE SCHEMA IF NOT EXISTS `parebemativos` ;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Aprovacao_Destino
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Aprovacao_Destino` (
  `aprdes_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `aprdes_observacao` VARCHAR(70) NULL DEFAULT NULL,
  `aprdes_dtinsercao` DATETIME NULL DEFAULT NULL,
  `aprdes_stativo` BIT(1) NULL DEFAULT NULL,
  `pes_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`aprdes_codigo`),
  INDEX `FK_Aprovacao_Destino_Pessoa` (`pes_codigo` ASC),
  CONSTRAINT `FK_Aprovacao_Destino_Pessoa`
    FOREIGN KEY (`pes_codigo`)
    REFERENCES `parebemativos`.`Pessoa` (`pes_codigo`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Aprovacao_Gerente
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Aprovacao_Gerente` (
  `apr_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `apr_observacao` VARCHAR(70) NULL DEFAULT NULL,
  `apr_dtinsercao` DATETIME NULL DEFAULT NULL,
  `apr_stativo` BIT(1) NULL DEFAULT NULL,
  `pes_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`apr_codigo`),
  INDEX `FK_Aprovacao_Gerente_Pessoa` (`pes_codigo` ASC),
  CONSTRAINT `FK_Aprovacao_Gerente_Pessoa`
    FOREIGN KEY (`pes_codigo`)
    REFERENCES `parebemativos`.`Pessoa` (`pes_codigo`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Ativos
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Ativos` (
  `ati_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `ati_descricao` VARCHAR(70) NULL DEFAULT NULL,
  `ati_estado` VARCHAR(30) NULL DEFAULT NULL,
  `ati_observacao` VARCHAR(150) NULL DEFAULT NULL,
  `ati_tag` VARCHAR(30) NULL DEFAULT NULL,
  `ati_marca` VARCHAR(30) NULL DEFAULT NULL,
  `ati_modelo` VARCHAR(30) NULL DEFAULT NULL,
  `ati_numeroSerie` VARCHAR(50) NULL DEFAULT NULL,
  `ati_stativo` BIT(1) NULL DEFAULT NULL,
  `tpa_codigo` INT(11) NOT NULL,
  `sal_codigo` INT(11) NOT NULL,
  `ati_valor` DECIMAL(6,2) NULL DEFAULT NULL,
  `ati_placa` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`ati_codigo`),
  INDEX `RefSala22` (`sal_codigo` ASC),
  INDEX `RefTipo_Ativo7` (`tpa_codigo` ASC),
  CONSTRAINT `RefSala22`
    FOREIGN KEY (`sal_codigo`)
    REFERENCES `parebemativos`.`Sala` (`sal_codigo`),
  CONSTRAINT `RefTipo_Ativo7`
    FOREIGN KEY (`tpa_codigo`)
    REFERENCES `parebemativos`.`Tipo_Ativo` (`tpa_codigo`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Endereco
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Endereco` (
  `end_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `end_referencia` VARCHAR(50) NULL DEFAULT NULL,
  `end_logradouro` VARCHAR(50) NULL DEFAULT NULL,
  `end_numero` INT(11) NULL DEFAULT NULL,
  `end_cep` VARCHAR(10) NULL DEFAULT NULL,
  `end_bairro` VARCHAR(30) NULL DEFAULT NULL,
  `end_cidade` VARCHAR(50) NULL DEFAULT NULL,
  `end_estado` VARCHAR(50) NULL DEFAULT NULL,
  `end_stativo` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`end_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 1026
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Filial
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Filial` (
  `fil_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `fil_razao` VARCHAR(70) NULL DEFAULT NULL,
  `fil_cnpj` VARCHAR(19) NULL DEFAULT NULL,
  `fil_stativo` BIT(1) NULL DEFAULT NULL,
  `end_codigo` INT(11) NOT NULL,
  `pes_codigo` INT(11) NOT NULL,
  `reg_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`fil_codigo`),
  INDEX `RefEndereco13` (`end_codigo` ASC),
  INDEX `RefPessoa17` (`pes_codigo` ASC),
  INDEX `RefRegional35` (`reg_codigo` ASC),
  CONSTRAINT `RefEndereco13`
    FOREIGN KEY (`end_codigo`)
    REFERENCES `parebemativos`.`Endereco` (`end_codigo`),
  CONSTRAINT `RefPessoa17`
    FOREIGN KEY (`pes_codigo`)
    REFERENCES `parebemativos`.`Pessoa` (`pes_codigo`),
  CONSTRAINT `RefRegional35`
    FOREIGN KEY (`reg_codigo`)
    REFERENCES `parebemativos`.`Regional` (`reg_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Motivo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Motivo` (
  `mot_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `mot_descricao` VARCHAR(70) NULL DEFAULT NULL,
  `mot_stativo` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`mot_codigo`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Parametrizacao
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Parametrizacao` (
  `nomeEmpresa` VARCHAR(50) NULL DEFAULT NULL,
  `cnpj` VARCHAR(50) NULL DEFAULT NULL,
  `logradouro` VARCHAR(50) NULL DEFAULT NULL,
  `numero` INT(11) NULL DEFAULT NULL,
  `bairro` VARCHAR(50) NULL DEFAULT NULL,
  `cidade` VARCHAR(50) NULL DEFAULT NULL,
  `telefone` VARCHAR(50) NULL DEFAULT NULL,
  `email` VARCHAR(50) NULL DEFAULT NULL,
  `dtCadastro` DATETIME NULL DEFAULT NULL,
  `logo` BLOB NULL DEFAULT NULL,
  `corElementos` VARCHAR(50) NULL DEFAULT NULL,
  `corTexto` VARCHAR(50) NULL DEFAULT NULL)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Pessoa
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Pessoa` (
  `pes_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `pes_matricula` VARCHAR(50) NULL DEFAULT NULL,
  `pes_nome` VARCHAR(70) NULL DEFAULT NULL,
  `pes_email` VARCHAR(50) NULL DEFAULT NULL,
  `pes_cargo` VARCHAR(50) NULL DEFAULT NULL,
  `pes_telefone` VARCHAR(16) NULL DEFAULT NULL,
  `pes_telefone2` VARCHAR(16) NULL DEFAULT NULL,
  `pes_stAtivo` BIT(1) NULL DEFAULT NULL,
  `end_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`pes_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 1011
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Regional
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Regional` (
  `reg_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `reg_descricao` VARCHAR(70) NULL DEFAULT NULL,
  `reg_stativo` BIT(1) NULL DEFAULT NULL,
  `pes_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`reg_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Sala
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Sala` (
  `sal_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `sal_descricao` VARCHAR(150) NOT NULL,
  `sal_stativo` BIT(1) NULL DEFAULT NULL,
  `fil_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`sal_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.Tipo_Ativo
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`Tipo_Ativo` (
  `tpa_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `tpa_descricao` VARCHAR(150) NULL DEFAULT NULL,
  `tpa_stativo` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`tpa_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 5
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.documentos
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`documentos` (
  `doc_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `transf_codigo` INT(11) NULL DEFAULT NULL,
  `doc_nome` VARCHAR(200) NOT NULL,
  `doc_local` TEXT NOT NULL,
  PRIMARY KEY (`doc_codigo`),
  INDEX `Reftranferencia28` (`transf_codigo` ASC),
  CONSTRAINT `Reftranferencia28`
    FOREIGN KEY (`transf_codigo`)
    REFERENCES `parebemativos`.`tranferencia` (`transf_codigo`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.imagem
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`imagem` (
  `img_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `img_dtinsercao` DATETIME NULL DEFAULT NULL,
  `ati_codigo` INT(11) NOT NULL,
  `img_imagem` LONGBLOB NULL DEFAULT NULL,
  `transf_codigo` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`img_codigo`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.itens_ativos
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`itens_ativos` (
  `transf_codigo` INT(11) NOT NULL,
  `ati_codigo` INT(11) NOT NULL,
  `img_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`transf_codigo`, `ati_codigo`, `img_codigo`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.localizacao
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`localizacao` (
  `loca_latitude` VARCHAR(70) NULL DEFAULT NULL,
  `loca_longitude` VARCHAR(70) NULL DEFAULT NULL,
  `img_codigo` INT(11) NOT NULL)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.logs
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`logs` (
  `log_descricao` VARCHAR(70) NULL DEFAULT NULL,
  `log_data` DATETIME NULL DEFAULT NULL,
  `log_responsavel` VARCHAR(30) NULL DEFAULT NULL)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.tipo_usuario
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`tipo_usuario` (
  `tpu_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `tpu_descricao` VARCHAR(70) NULL DEFAULT NULL,
  `tpu_nivel` INT(11) NULL DEFAULT NULL,
  `tpu_stativo` BIT(1) NULL DEFAULT NULL,
  PRIMARY KEY (`tpu_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.tranferencia
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`tranferencia` (
  `transf_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `transf_dtfechamento` DATETIME NULL DEFAULT NULL,
  `transf_dtabertura` DATETIME NULL DEFAULT NULL,
  `transf_observacao` VARCHAR(70) NULL DEFAULT NULL,
  `transf_stativo` BIT(1) NULL DEFAULT NULL,
  `fil_codigo` INT(11) NOT NULL,
  `fil_codigo_destino` INT(11) NOT NULL,
  `mot_codigo` INT(11) NOT NULL,
  `aprdes_codigo` INT(11) NULL DEFAULT NULL,
  `apr_codigo` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`transf_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 41
DEFAULT CHARACTER SET = latin1;

-- ----------------------------------------------------------------------------
-- Table parebemativos.usuario
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `parebemativos`.`usuario` (
  `user_codigo` INT(11) NOT NULL AUTO_INCREMENT,
  `user_login` VARCHAR(50) NULL DEFAULT NULL,
  `user_senha` VARCHAR(50) NULL DEFAULT NULL,
  `tpu_codigo` INT(11) NOT NULL,
  `pes_codigo` INT(11) NOT NULL,
  PRIMARY KEY (`user_codigo`))
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = latin1;
SET FOREIGN_KEY_CHECKS = 1;
