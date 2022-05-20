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

Parseser が出来上がって、xtumlmc_schema.sql をパースしたところ  

- Table (概念クラス) 382
- Relationship  508 (うち、2項:437、Super・Sub:45、RelationshipClass付き:26)

が定義されている事が判明。
その後、それぞれの概念クラス、Relationship を C#クラス化するためのフレームワークライブラリを、「BridgePoint の OOA of OOA ならこんな感じだよねぇ…」といった既知の事項を元に検討しつつ、ざっくり定義。大体0.5日。

![1st draft](./images/ooa-of-ooa-framework-library-1st-draft.svg)

こんな感じで先ず定義。  
その後、xtumlmc_schema.sql の一部を使ってプロトタイピングを実施し、各 TABLE、Relationshipの定義を具象クラスに変換するルールを試行錯誤しながら決めていく。  


※ この作業は自動化するしないに関わらず必要。約１日を要した。

自動生成するので、生成ルールの作成を開始。先ず、CIC<i>ClassName</i> に当たるインターフェイス（結果的に382のインターフェイスを宣言しなければならないわけだ）の変換ルールを作成。結果的に生成されたファイルは、[CIClassDefs.cs](./Kae.CIM.MetaModel.CIMofCIM/CIClassDefs.cs) なのだが、変換ルールを作成するのと並行して、自動生成に必要なロジックも併せて開発。作成で約0.5日。やってみて分かったことを元に、フレームワークライブラリの修正をしつつ変換ルールのテストを実施。この作業が大体0.5日。  
次に、CIC<i>ClassName</i>Base の変換ルールを作成しつつ、フレームワークライブラリの修正もしつつ（コードへの変換を楽になるようにとか）作業を進めた。結果的に生成されたファイルは、[CIClassBase.cs](Kae.CIM.MetaModel.CIMofCIM/CIClassBases.cs)。作成に約0.5日。

これもファイナルではないのだが、（後は、実際には作ってもあまりメリットはないので本当の開発作業なら作らないが）修正した後のフレームワークライブラリを参考までに図示する。  

![2nd draft](./images/ooa-of-ooa-framework-library-2nd-draft.svg)  

実装クラスを生成してみて改めて判った修正点を CIClassDefs.cs の変換ルールも見直し・修正しつつ、生成テストを実施。具体的には生成したコードがコンパイルエラーを出さない事が目安。この作業が大体約0.5日。  

ここまでで、自動生成の為に費やした開発日数は、計約3日。  
参考までに、自動生成せずに手作業でのんびりと作業を進めた場合にかかる時間と自動生成を取り入れた作業時間の違いを比較しておく。  
一つの TABLE 定義から2つの C# クラスが作成され、まぁ大体5分位一つの TABLE 定義への対応がかかるとし、一つの Relationship は、二項かスーパーサブか RelationshipClass かで諸々実装ルールが変わるもののざっくり一つあたり5分（実際には10分以上かかるものもありそうだが）と、係る時間は、  
382 × 5 分 + 508 × 5 分 = 4450 分 ≒ 74時間 ≒ 12 日  
※1日当たりの実働を6時間とする。上に挙げた日数の単位と同じ。
と見積もられ、9日間の短縮となっている。この段階で、手作業に比べて、4倍の効率となっている。今後、作業が進むにしたがってさらなる見直しが発生することは火を見るより明らかなので、自動生成は、どんどん効率化の度合いが大きくなっていくことが判る。  

この時点で、2022/5/18 の日暮れ。残りは、CIModelRepsitory クラスの実装の部分と、BridgePoint で作成したモデルを作成したライブラリに入れ込む部分の開発。

次に、出来上がった OOA of OOA のクラスライブラリ群の生成、一覧利用を行うための、CIModelRepository を仕上げる作業を行う。現時点での実装は、[CIModelRepository.cs](Kae.CIM.MetaModel.CIMofCIM/CIModelRepository.cs) で、一応の定義となっている。  
> …で、作業を進めるその前に、このリポジトリには、各概念クラスのインスタンスを、概念モデリングの流儀に従って検索する手段が用意されていない。より具体的に言えば、「概念クラスは、アイデンティティを保持するためのプロパティがあるので、そのプロパティ値を指定して、該当する概念インスタンスを取り出せてもいいんじゃないか？」という事。しかし、結局このリポジトリに格納されるのは、BirdgePoint で作成した、ある特定のドメインに対する UML モデル（私流にいえば<b>概念モデル</b>）の定義全てであり、その入力された情報を何に使うかと言えば、その入力情報を元に、開発活動において必要ななにがしかの開発成果物への変換用途としてしか使わないので、結局、全ての情報を総なめする事しかしないので、結局、アイデンティティベースの検索手段は必要ない。

CIModelRepository は、それぞれの概念クラスのインスタンスを作成するメソッドを提供している。生成した時にユーザーコードが取得できるのは、CIM<i>ClassName</i>Def という、その概念クラスが持つプロパティ一式への読み書き用 Property 一式と、その概念クラスが関与する Relationship に基づいて Link されている概念インスタンス群を取得する Method 群一式を提供するインターフェイスである。CIModelRepository の各 Method の実装は、[CIModelRepositoryImpl](Kae.CIM.MetaModel.CIMofCIM/CIModeRepsoitoryImpl.cs) で行っている。作成済みの概念インスタンス群を取り出すのと、リポジトリからの削除は、まぁ簡単ではあるが、概念クラスのインスタンスを生成（つまり、CIM<i>ClassName</i>>Baseクラスのコンストラクターを使って新しいインスタンスを作成する）する部分は、具体的な型をなんとかして実装ロジック内で使わなければならなくなる。

※ 書くと簡単だが、xtUML の OOA of OOA 概念クラスは何しろ 382 個もあるわけで、手書きなら見通しも悪いし、className を使って switch case 等使ったら、1000行以上のコードになってしまう。自動生成なら簡単ではあるが、たとえ自動生成であっても（まぁ、自動生成だからこそか？）シンプルで簡潔なコードにするのは大原則なので、どう実装するかは十分に検討しなければならない。  

解決策として、[C# の Reflection](https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/concepts/reflection) を使う事に決定。
CIModelRepositoryImpl.CreateCIInstance() メソッドの定義は、複数の概念クラスに対応できるように Generics を使って定義されている。  
```C#
T CreateCIInstance<T>(string domainName, string className, IDictionary<string, object> attributes) where T: CIClassDef;
```
この宣言の意味するところは、このメソッドが CIClassDef を実装するクラスのインスタンスを返すということであり、引数で生成する概念クラスの名前(className)が供給されることになっている。  
このクラス名から、'CIM<i>className</i>Base' という命名規則で、どの C# 上のクラスのコンストラクターを呼べばいいかわかる。更に、CreateCIInstance メソッド内で、自分が所属しているアッセンブリーが持っている他のクラス群を Reflection によって見つけることができるので、結果として
```C#
    string typeName = $"CIMClass{className}Base";
    var currentMethod = MethodBase.GetCurrentMethod();
    var assembly = currentMethod.DeclaringType.Assembly;
    var candidates = assembly.GetTypes().Where( t => t.IsClass && t.Name == typeName);
    if (candidates.Count() > 0)
    {
        var cClass = candidates.First();
        T cInstance = (T)cClass.GetConstructor(new Type[] { typeof(CIModelRepository), typeof(IDictionary<string, object>) }).Invoke(new object[] { this, attributes });
```
といったロジックを書けば、一切、概念クラスの実装クラスを明示しなくても、インスタンスを作成することができる。たかだか 10 行程度で実装は完成する。  

> 繰り返しになるが、設計、コーディングにおいては、なるべく、やりたいことの本質を見出し、シンプルに短く簡潔にコードを仕上げることを常に意識する事。これは手書きでも自動生成でも同じである。手書きの場合は手間が劇的に減り、自動生成の場合は、自動生成ルール作成の手間が劇的に減る。  

ここまでで、2022/5/19 のお昼間近く。なので作業時間は、0.5 日。
後は、BridgePoint で作ったモデル定義を、このCIM of CIM ライブラリに入れ込むところを作れば、ゴール。  

OOA of OOA の 概念クラス群から生成したモデルフレームワークの CIM<i>ClassName</i>Def クラスのインスタンスをロードする為のパーサーと CIModelRepository にロードするロジックを作成し、BridgePoint をインストールすると自動的に用意される Microwave Oven のモデルを使って、テストを実施し完了。作業時間は、約 0.75 日。  
実際にインスタンスを作った時の不便さ等から、CIModelRepository のインターフェイスを若干変更。  
実際にやってみたところ、Microwave Oven のモデルイインスタンス素には、概念クラスが定義されていないもの多数あることを確認。モデルの図表示に関する概念クラスが無いようだ。まぁ問題ないので無視。結果的に、  

- 定義済みの概念クラスへのロードされたインスタンス総数 - 685
- 未定義の概念クラスのインスタンス総数 - 892  

という結果を得る。ユーザーが作成する概念情報モデルの規模が大きくなる（概念情報クラスの数、プロパティ数、Relationshipの数など）と、必然的に増えていく。手作業の場合は、図を見ながら、あるいは、エクスポートされた XTUML ファイルの内容をコピペしながら、ロードするロジックを描くか、入力していく GUI アプリを作って手入力していくかのどちらかの作業が必要になる。ざっくり、  

- 定義済みか未定義の判別にかかる時間はとりあえず無視
- 定義済みの一つのインスタンスあたり、3分程度の作業  

とすると、685 × 3 分 ＝ 2055 分 ≒ 34 時間 ≒ 5.7 日 かかることになる。  
たかだか、Microwave Oven 程度のモデルでこのぐらいかかるとすると、手作業でやるのは、不可能であろう。  

…と、ここまで書いてきて、CIM<b>ClassName</b>Base クラスのコンストラクターで、引数をプロパティに代入するコードを生成するのを忘れている事に気づく。（不覚）  
一旦、Github を更新して、その作業を行う。 現在 2022/5/20 11時

修正約 10 分でコンストラクターの不具合修正完了。実行動作確認は行っていないが、生成コードのビルドはできたので、一旦リリース 。 現在 2022/5/20 11時10分  
コード自動生成の場合は、ありがちな不具合。手書きコードの様に、たまに間違ってしまったものを発見しながら直していくとか、全部間違っているのを見つけながら直していくのとは違い、コード自動生成の場合は、全部間違っていたものを一括して全て直すことになる。また、気軽に全コード更新が可能なので、気が緩みがちなので注意が必要。  

> 今回の様な、実装クラスのコンストラクタでの、引数で渡されたデータ群をプロパティに代入しない間違いは、実開発では、先ず発生しないかもしれない。手書きコード作業主体の、ある程度の大人数が関わるプロジェクトで脳内実験をしてみる。  
先ず、設計を主導する上級エンジニアが、アーキテクチャやフレームワークを設計して、実装ガイドを作成し、それを見ながら、複数のエンジニアが、それぞれの担当部分をガイドに沿ってコーディングを行う事になるだろう。仮に、実装ガイドが間違っていた場合、あるいは、ありがちな状況として、上級エンジニアが、「コンストラクターで渡された引数、それも、プロパティ群と一対一対応する変数ならば、当然、それぞれのプロパティに代入するコードを書くだろう」という想定で、実装ガイドに記載していない（ありそう？）、手書きコーディングを行うエンジニアは、日々の間接業務に疲弊して、碌に頭を使わずに、実装ガイドに書かれている通りにしかコーディングしなかった（これもありそうだ）、これらの結果として、当然実装しなければならないロジックが抜け落ちる、ということは十分ありうるのではないかと思われる。  
これは当然、テストで発覚する訳であるが、大人数での開発の場合は、複数人の作業成果物が揃った時点でテストが行われるので、一番作業が遅かったエンジニアの作業完了時点に拘束される。そこから修正作業が開始され、382 クラスのコンストラクターを修正しなければならなくなる。それにかかる時間を、一コンストラクターあたり最速 1 分掛かるとしても約1日がかかってしまう。それだけでなく、テストがホワイトボックス的なテストを実施している場合は、多分、不具合の原因を発見するのは非常に難しいのではないだろうか？「まさかそんなことしてないよね？」的なコーディングによる障害の原因発見は経験的になかなかに難しい。また、テスト時点で、作業の早いエンジニア達は既に次の作業にとりかかっているはずなので、プロジェクト全体への影響は、案外大きいのではないだろうか？  

2022/5/20 15:30  
xtUML（Shlaer Mellor法）の OOA of OOA を元にメタモデルフレームワークライブラリを生成し、BridgePoint で作成したモデルを元に概念情報インスタンスをプログラム上で保持するロジックが一通り完成したので、これまで開発に使っていた Console Application をクラスライブラリ化。  
※ [Kae.XTUML.Tools.MetaModelGeneretor](Kae.XTUML.Tools.MetaModelGenerator/)の下のCSPROJファイルの名前が、よりふさわしい名前に変えた（現時点で、Kae.XTUML.Tools.CIModelResolver）ことにより変更されている。  
この変更に伴い、新たに、[SampleModelGenerator](./SampleModelGenerator/) というプロジェクトを作成し、テスト用 Console Application とした。  

```C#
            var recognizer = new ConceptualInformationModelResolver(commandLine.MetaModelFilePath);
            try
            {
                Console.WriteLine($"Loading OOA of OOA model... @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                recognizer.LoadOOAofOOA(commandLine.DataTypeDefFilePath);
                Console.WriteLine($"Loaded.  @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");

                if (commandLine.GenerateFWLib && !string.IsNullOrEmpty(commandLine.GenFolderPath))
                {
                    Console.WriteLine($"Generating Meta Model Framework Library...  @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                    recognizer.GenerateCIMFramework(commandLine.GenFolderPath).Wait();
                    Console.WriteLine($"Generated.   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                }
                if (!string.IsNullOrEmpty(commandLine.InstancesFile))
                {
                    Console.WriteLine($"Loading instances...   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                    recognizer.LoadCIInstances(commandLine.InstancesFile, true);
                    Console.WriteLine($"Loaded.   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var ciinstances = recognizer.ModelRepository.GetDomainCIClasses("OOAofOOA");
            Console.WriteLine($"Count - {ciinstances.Keys.Count}");
```
この様な流れで、BridgePoint で作成した概念モデルの要素を全て解釈してプログラム上で扱える形式に変換できたので、それを元に、  
- Azure Digital Twins 向け、DTDL ファイル  
- IoT 機器アプリコード
- クラウドサービス側の各種ロジック  

等の自動生成を行う変換ルールを作って足してやればよい。

2021/5/20 の時点で、若干スパゲッティコード化しているので、今後、適宜、リファクタリングを行っていく。また、フレームワークライブラリ生成後のビルド起動が、未実装なので、適宜行っていく。  
