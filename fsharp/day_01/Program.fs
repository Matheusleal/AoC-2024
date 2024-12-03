open Swensen.Unquote
open System
open System.IO

let getData path = 
    File.ReadAllLines(path)
    |> List.ofSeq

let parse (lines: string list) = 
    lines
    |> List.map (fun line -> line.Split("   ") |> Seq.map int |> Seq.toList)
    |> List.transpose

let order collection = 
    collection
    |> List.sort

let listToMap (collection: int list) =
    let addElement (acc:Map<int,int>) element = 
        match acc.TryGetValue(element) with
        | true, value -> acc.Add(element, value + 1)
        | _ -> acc.Add(element, 1)
    
    (Map.empty, collection)
    ||> Seq.fold (addElement)

let calculateTotalDistance (left : int list) (right : int list) = 
    List.zip left right
    |> List.sumBy (fun (l, r) -> abs (l - r))

let calculateTotalSimalarityScore (left : int list) (right : Map<int,int>) =
    left
    |> List.sumBy (fun l -> if right.ContainsKey l then l * right.[l] else 0)
    

let [left: int list; right: int list] = 
    getData "data/location_id_list.txt"
    |> parse

let leftList = order left
let rightList = order right
let rightMap = listToMap right


let totalDistanceScore = calculateTotalDistance leftList rightList
let totalSimalarityScore = calculateTotalSimalarityScore leftList rightMap

Console.WriteLine("Total Distance score: {0}", totalDistanceScore)
Console.WriteLine("Total Simalarity score: {0}", totalSimalarityScore)