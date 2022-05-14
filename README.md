# xtUML の OOA of OOA モデルを C# ライブラリ化する  
[BridgePoint](https://github.com/xtuml/bridgepoint) のモデルコンパイラ向けに定義されている、OOA of OOA モデル（[Art of Conceptual Modeling](https://note.com/kae_made/m/m054c9f9f8b61)で解説している<b>概念情報モデルの概念情報モデル</b>と同等）を、C# アプリケーションで利用可能にするための、クラスライブラリを作成して、公開する。  
イメージとしては、[DTDL(Digital Twin Definition Language) Parser](https://docs.microsoft.com/en-us/azure/iot-develop/concepts-model-parser) の、Microsoft.Azure.DigitalTwins.Parser 名前空間で提供されいる一連の DT<i>Entity</i>Info という名前を持つクラス群の様なもの。  
当初、大枠の変換ルールを決めて、手動で、クラスを定義しようと思ったが、元になる、xtumlmc_schema.sql をざっと眺めて、4000行近くあり量が多いので、これは、[Essense of Software Design](https://note.com/kae_made/m/m2e74d05de8b0) や [Practices of Software Engineering](https://note.com/kae_made/n/n624e6da0f930) で紹介している、ソフトウェア開発成果物自動生成の格好のサンプルだな、ということで、Translator を作って、クラス定義を自動化する事にした。  
そうなると、xtumlmc_schema.sql を、字句解析、構文解析する仕組みが必要になる。  
このファイルをざっと眺めると、大きく分けて、  

- SQL 文法標準の TABLE 定義
- SQL 文法の拡張と思われる Relationship の定義

が見受けられる。どちらも一行完結の比較的シンプルな文法なので、正規表現等を駆使して、最悪、一から自作しても構わないレベルなのだが、SQL 文法標準であれば、既存の SQL Parser を探して使えばいいし、Bridgepoint のソースコードのどこかにありそうなので、それを探して使えばいい…とも思ったが、ここは、自分の字句解析、構文解析の最新技術の習得による自動変換系のスキルアップを兼ねて、最新の最もよく使われていると思われるオープンソースを使って Parser を作ることにした。  
当初、[Roslyn](https://docs.microsoft.com/ja-jp/dotnet/csharp/roslyn-sdk/) ？ と思ったが、これは C# 専用のパーサーなので（確かベースはマルチ言語だったような気が…）、あれこれ探して、Unix 系ではど定番、かつ、古来より伝わる、lex、yacc を、C# と組み合わせて使える、[YaccLexTools](https://www.nuget.org/packages/YaccLexTools/)
 を見つけたので、そういえば四半世紀前によく使ってたな…等と思いながら選定。  
 しかし、ドキュメントほぼなし、サンプル希少につき、使い方が当初判らず難航。  
 一日強、あーだこーだやって、漸く使い方のコツをつかみ、スキーマ定義ファイルをパースして、C# プログラム上で扱えるように用意したデータ構造に全てを格納するところまでを完成(@2022/5/14)し、一旦公開。
 → [Kae.XTUML.Tools.MetaModelGenerator](Kae.XTUML.Tools.MetaModelGenerator/)  
※ [./Solution.sln](./Solution.sln) を Visual Studio で開きビルドする。  
※ xtumlmc_schema.sql のフルパスを第一引数として起動する。  

ちなみに、LexYaccTools を使う肝は、「Lex 側でパターンにマッチした字句に対して、対応する Token の要素をリターンするコードを書くこと」これをやらないと、Yacc 側の構文木定義で字句として使えない。  
